using SQLite;
using KingTranslator;
using System;

namespace KingTranslator
{
    public class TranslatorItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Phrase { get; set; }
        public string PhraseLanguage { get; set; }
        public string TranslatedPhrase { get; set; }
        public string TranslatedPhraseLanguage { get; set; }
        public string Pinyin { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is TranslatorItem)
            {
                var o = (TranslatorItem)obj;
                return o.ID == this.ID;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}