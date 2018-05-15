using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID.Common
{
    public class DataBase
    {
        private static readonly string ConnectionStringTemplate = "Data Source={0};Version=3;";

        public string DBFile { get; }

        private SQLiteConnection m_dbConnection;

        public DataBase(string file)
        {
            this.DBFile = file;
            this.Open();
        }

        private void Open()
        {
            bool createDB = false;
            if (!File.Exists(this.DBFile))
            {
                SQLiteConnection.CreateFile(this.DBFile);
                createDB = true;
            }

            this.m_dbConnection = new SQLiteConnection(String.Format(ConnectionStringTemplate, this.DBFile));
            this.m_dbConnection.Open();

            if (createDB)
            {
                this.BuildDatabaseStructure();
            }
        }

        public void Execute(string query, params object[] args)
        {
            SQLiteCommand command = new SQLiteCommand(String.Format(query, args), m_dbConnection);
            command.ExecuteNonQuery();
        }

        public SQLiteDataReader Query(string query, params object[] args)
        {
            SQLiteCommand command = new SQLiteCommand(String.Format(query, args), m_dbConnection);
            return command.ExecuteReader();
        }

        /* ***************************************************************************************************** */

        private void BuildDatabaseStructure()
        {
            this.Execute("CREATE TABLE Checkins (Date TEXT, Card VARCHAR(10))");
        }

        public void Checkin(string card)
        {
            string date = DateTime.Now.ToString(CheckinEntry.DATETIME_STRING_FORMAT);
            this.Execute("INSERT INTO Checkins (Date, Card) VALUES ('{0}', {1})", date, card);
        }
    }
}
