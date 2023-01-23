using Avalonia;
using DynamicData;
using Microsoft.Data.Sqlite;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Todo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Todo.Services
{
    public class Database
    {

        private SqliteConnection connection;

        public SqliteConnection Connection { get { return connection; } }
        
        public void Init()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            connection = new SqliteConnection(LoadConnectionString());
            connection.Open();

            string tableCreate = "CREATE TABLE IF NOT EXISTS \"todos\" (Id INTEGER, Description TEXT, IsChecked INTEGER, PRIMARY KEY(\"Id\" AUTOINCREMENT))";

            SqliteCommand command = new(tableCreate, connection);
            _ = command.ExecuteNonQuery();
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        public void AddItem(string Description, int IsChecked)
        {
            connection = new SqliteConnection(LoadConnectionString());
            connection.Open();
            string cmd = "INSERT INTO todos (Description, isChecked) VALUES ($Description, $IsChecked)";
            SqliteCommand sqlCmd = new SqliteCommand(cmd, connection);
            sqlCmd.Parameters.AddWithValue("$Description", Description);
            sqlCmd.Parameters.AddWithValue("$IsChecked", IsChecked);
            _ = sqlCmd.ExecuteNonQuery();
        }

        public List<TodoItem> LoadAll()
        {
            connection = new SqliteConnection(LoadConnectionString());
            connection.Open();
            string cmd = "SELECT * FROM todos";
            SqliteCommand sqlCmd = new(cmd, connection);
            var rdr = sqlCmd.ExecuteReader();
            List<TodoItem> values = new();
            while (rdr.Read())
            {
                int todoId = rdr.GetInt32(0);
                string todoDesc = rdr.GetString(1);
                bool todoChecked = (rdr.GetInt32(2) == 1) ? true : false;

                values.Add(new TodoItem { Id = todoId, Description = todoDesc, IsChecked = todoChecked });
                


            }
            return values;
        }


        public void DeleteItem(int Id) { 
            connection = new SqliteConnection(LoadConnectionString());
            connection.Open();

            string cmd = "DELETE FROM todos WHERE Id=" + Id;
            SqliteCommand sqlCmd = new(cmd, connection);
            _ = sqlCmd.ExecuteNonQuery();


        }



        public IEnumerable<TodoItem> GetItems() => LoadAll();
       
    }
}