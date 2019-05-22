using KingTranslator.WinPhone;
using SQLite;
using System;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_WinPhone))]


namespace KingTranslator.WinPhone
{
    public class SQLite_WinPhone : ISQLite
    {
        public SQLite_WinPhone()
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