using KingTranslator.iOS.MicrosoftTranslator;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace KingTranslator.iOS
{
    public class Translator : ITranslator
    {
        static Translator translator = new Translator();
        public static Translator Default { get { return translator; } }

        // Developer application ID
        private readonly string APP_ID = "C17425D9664591D2CFF5AFCC91A527993BD5AE59";

        // Microsoft Translator Service Client
        private SoapService client;

        private TranslatorItem phrase;

        // Constructor
        protected Translator()
        {
            if (client == null)
            {
                client = new SoapService();
                client.TranslateCompleted += client_TranslateCompleted;
            }
        }

        private void client_TranslateCompleted(object sender, TranslateCompletedEventArgs e)
        {
            Console.WriteLine(e.Result);
            phrase.TranslatedPhrase = e.Result;

            // If Chinese, generate pinyin
            if (phrase.TranslatedPhraseLanguage == Language.Chinese.GetStringValue())
            {
                HttpWebRequest request = WebRequest.Create(new System.Uri(string.Format("http://www.popupchinese.com/adso/pinyin.php?text={0}", e.Result))) as HttpWebRequest;
                request.BeginGetResponse(new AsyncCallback(SavePinyin), request);
            }
            else
            {
                HttpWebRequest request = WebRequest.Create(new System.Uri(string.Format("http://www.popupchinese.com/adso/pinyin.php?text={0}", phrase.Phrase))) as HttpWebRequest;
                request.BeginGetResponse(new AsyncCallback(SavePinyin), request);
            }
        }

        private void SavePinyin(IAsyncResult ar)
        {
            HttpWebRequest asyncState = (HttpWebRequest)ar.AsyncState;
            HttpWebResponse response = null;

            try
            {
                response = asyncState.EndGetResponse(ar) as HttpWebResponse;
            }
            catch (WebException exception)
            {
                //MessageBox("King Translator", exception.Message);
                Console.WriteLine(exception.Message);
                return;
            }

            string str = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                str = reader.ReadToEnd();
            }

            if (str.Length > 0)
            {
                string[] strArray = str.Split(new char[] { '\'' });

                if (strArray.Length > 1)
                {
                    try
                    {
                        phrase.Pinyin = System.Web.HttpUtility.HtmlDecode(strArray[1]);
                    }
                    catch (ArgumentOutOfRangeException exception)
                    {
                        //MessageBox("King Translator", exception.Message);
                        return;
                    }
                }
            }

            Console.WriteLine(phrase.Pinyin);

            // This will call listener to populate the field in the TranslatePage of the App
            App.TranslatorManager.Phrase = phrase;
        }

        public void TranslatePhrase(TranslatorItem item)
        {
            this.phrase = item;

            client.TranslateAsync(APP_ID, item.Phrase, "", item.TranslatedPhraseLanguage, "text/plain", "general", "");
        }

        public Task DeleteFromDictionary(TranslatorItem id)
        {
            throw new NotImplementedException();
        }

        public Task SaveToDictionary(TranslatorItem item)
        {
            throw new NotImplementedException();
        }
    }
}
