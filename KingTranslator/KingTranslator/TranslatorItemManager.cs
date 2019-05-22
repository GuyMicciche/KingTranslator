using System;
using System.Threading.Tasks;

namespace KingTranslator
{
    public class TranslatorItemManager
    {
        //////////////////////////////////////////////////////////////////////////
        // EVENTS Section
        //////////////////////////////////////////////////////////////////////////

        /************************************************************************/
        /* Language listener                                                    */
        /************************************************************************/
        public delegate void TranslationChangedEventHandler(object sender, TranslationChangedArgs e);
        public class TranslationChangedArgs : EventArgs
        {
            private TranslatorItem phrase;

            public TranslationChangedArgs(TranslatorItem phrase)
            {
                this.phrase = phrase;
            }

            public TranslatorItem Phrase
            {
                get
                {
                    return phrase;
                }
            }
        }

        public event TranslationChangedEventHandler TranslationChanged;
        protected virtual void OnTranslationChanged(TranslationChangedArgs e)
        {
            TranslationChanged(this, e);
        }

        private TranslatorItem phrase;
        public TranslatorItem Phrase
        {
            get
            {
                return this.phrase;
            }
            set
            {
                this.phrase = value;

                TranslationChangedArgs args = new TranslationChangedArgs(value);
                this.OnTranslationChanged(args);
            }
        }

        private ITranslator translator;

        public TranslatorItemManager(ITranslator msTranslator)
        {
            translator = msTranslator;
        }

        public void TranslatePhrase(TranslatorItem item)
        {
            translator.TranslatePhrase(item);
        }

        public Task SaveToDictionary(TranslatorItem item)
        {
            return translator.SaveToDictionary(item);
        }

        public Task DeleteFromDictionary(TranslatorItem item)
        {
            return translator.SaveToDictionary(item);
        }
    }
}