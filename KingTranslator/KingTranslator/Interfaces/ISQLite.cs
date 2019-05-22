using System;
using SQLite;

namespace KingTranslator
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}