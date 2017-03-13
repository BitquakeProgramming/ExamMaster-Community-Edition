using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using ExamMaster.Config;
using ExamMaster.Backend;

namespace ExamMaster.Database
{
    public class DB:IDisposable : IDisposable
    {
        private MySqlConnection connection;
        public void GetDataTest()
        {
        }
        public void OpenConnection()
        {
            string connectionString = "datasource=localhost;database=binnenschifffahrt;username=root;password=";
            connection = new MySqlConnection(connectionString)
            connection.Open();
        }

        public QuestionCatalog FillCatalog(CatalogModel model, int varId)
        {
            string query = "SELCET * FROM " + model.SQLName + " WHERE " + model.SQLVariationName + " = " + varId;
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            foreach(DataRow row in table.Rows){
                int taskID = (Int32)row[model.SQLTaskName];

                // DEEPER *~*
                MySqlCommand cmd2 = new MySqlCommand(query, connection);
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                DataTable table2 = new DataTable();
                table2.Load(reader2);
                foreach(DataRow row2 in table2.Rows)
                {

                }
            }

            cmd.Dispose();

        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void GetData()
        {
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool managed)
        {
            if(managed){
                connection.Dispose();
            }
        }

        ~DB(){
            Dispose(false);
        }
    }
}
