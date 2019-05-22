using KingTranslator.Win;
using SQLite;
using System;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Win))]


namespace KingTranslator.Win
{
    public class SQLite_Win : ISQLite
    {
        public SQLite_Win()
        {

        }

        public SQLiteConnection GetConnection()
        {
            var fileName = "TranslatorItems.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);

            var connection = new SQLiteConnection(path);

            return connection;
        }
    }
}