using System;
using System.IO;
using SQLite;

using Xamarin.Forms;
using KingTranslator.iOS;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace KingTranslator.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS()
        {

        }

        public SQLiteConnection GetConnection()
        {
            var fileName = "TranslatorItems.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);

            var connection = new SQLiteConnection(path);

            return connection;
        }
    }
}