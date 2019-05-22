using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using Xamarin.Forms;

namespace KingTranslator
{
    public class Database
    {
        private SQLiteConnection database;

        public Database()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<TranslatorItem>();
        }

        public IEnumerable<TranslatorItem> GetPhrases()
        {
            return (from t in database.Table<TranslatorItem>()
                    select t).ToList();
        }

        public TranslatorItem GetPhrase(TranslatorItem phrase)
        {
            return database.Table<TranslatorItem>().FirstOrDefault(p => p.Phrase == phrase.Phrase);
        }

        public bool PhraseExists(TranslatorItem phrase)
        {
            return database.Table<TranslatorItem>().Any(p => p.Phrase == phrase.Phrase);
        }

        public void DeletePhrase(int id)
        {
            database.Delete<TranslatorItem>(id);
        }

        public void AddPhrase(TranslatorItem phrase)
        {            
            database.Insert(phrase);
        }

        public IEnumerable<TranslatorItem> GetAllPhrases()
        {
            List<TranslatorItem> query = new List<TranslatorItem>();

            query = new List<TranslatorItem>(from p in database.Table<TranslatorItem>() orderby p.ID select p);

            return query;
        }

        public IEnumerable<TranslatorItem> GetPhrasesByLanguage(Language language)
        {
            List<TranslatorItem> query = new List<TranslatorItem>();
            string lang = language.GetStringValue();

            query = new List<TranslatorItem>(from p in database.Table<TranslatorItem>()
                                             where p.TranslatedPhraseLanguage == lang
                                             orderby p.ID
                                             select p);

            return query;
        }
    }
}
