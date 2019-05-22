using System;
using System.IO;
using Xamarin.Forms;
using SQLite;
using KingTranslator.Android;

[assembly: Dependency(typeof(SQLite_Android))]

namespace KingTranslator.Android
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {

        }

        public SQLiteConnection GetConnection()
        {
            var fileName = "TranslatorItems.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var connection = new SQLiteConnection(path);

            return connection;
        }
    }
}