namespace KingTranslator
{
    public class Phrase
    {
        public int ID { get; set; }
        public string OriginalPhrase { get; set; }
        public string OriginalPhraseLanguage { get; set; }
        public string TranslatedPhrase { get; set; }
        public string TranslatedPhraseLanguage { get; set; }
        public string Pinyin { get; set; }
    }
}