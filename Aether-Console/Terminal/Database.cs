using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;

namespace Aether_Console.Terminal
{
    public class Database
    {
        private const string DatabaseFile = "databaseFile.db";
        private const string DatabaseSource = "data source=" + DatabaseFile;
        private SQLiteConnection dbConnection;

        public void createDatabase()
        {
            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);
            }
        }

        public void doTableOrInsert(string sql)
        {
            dbConnection = new SQLiteConnection(DatabaseSource);
            dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public void getJoke(string sql)
        {
            dbConnection = new SQLiteConnection(DatabaseSource);
            dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine(reader["joke"]);
            dbConnection.Close();
        }
    }
}