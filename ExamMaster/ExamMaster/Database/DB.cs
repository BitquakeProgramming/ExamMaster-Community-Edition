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
using System.Windows.Forms;
using ExamMaster.Backend;
using ExamMaster.Frontend;

namespace ExamMaster.Database
{
    public class DB : IDisposable
    {
        private MySqlConnection connection;
        private bool connectionOpen = false;
        
        /// <summary>
        /// Opens a connection using the CatalogModel.
        /// </summary>
        /// <param name="model">The model contains all connection informations</param>
        /// <returns>true, if the connection was opened successfully</returns>
        public bool OpenConnection(CatalogModel model)
        {
            StringBuilder connectionBuilder = new StringBuilder();
            connectionBuilder.Append("server=").Append(model.DataSource).Append(";");
            if(model.Port > 0)connectionBuilder.Append("port=").Append(model.Port).Append(";");
            connectionBuilder.Append("database=").Append(model.Database1).Append(";");
            connectionBuilder.Append("username=").Append(model.Username).Append(";");
            connectionBuilder.Append("password=").Append(model.Password);
            string connectionString = connectionBuilder.ToString();
            try
            {
                if(connectionOpen)throw new NotSupportedException("Close the database connection before opening another one!");
                connection = new MySqlConnection(connectionString);
                connection.Open();
                connectionOpen = true;
                return true;
            }
            catch (Exception e)
            {
                ExceptionHandler.Throw(e, "Es konnte keine Verbindung zur Datenbank Hergestellt werden!");
            }
            return false;
        }

        /// <summary>
        /// Fills a predefined QuestionCatalog with data from the database, again using the CatalogModel which contains
        /// all required table and field names.
        /// </summary>
        /// <param name="catalog">All the Questions will be loaded into this list class.</param>
        /// <param name="model">The model contains all connection informations</param>
        /// <param name="varId"></param>
        /// <returns></returns>
        public bool FillCatalogBinnen(ref QuestionCatalog catalog, CatalogModel model, int varId)
        {
            try { 
                string query1 = "SELECT * FROM " + model.SQLCatalogName + " WHERE " + model.SQLVariationName + " = " +
                                varId;
                MySqlCommand cmd = new MySqlCommand(query1, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                foreach (DataRow row in table.Rows)
                {
                    int taskID = (Int32) row[model.SQLTaskName];

                    // DEEPER *~*
                    string query2 = "SELECT * FROM " + model.SQLTaskDbName + " WHERE " + model.SqlTaskId + " = " +
                                    taskID;
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    DataTable table2 = new DataTable();
                    table2.Load(reader2);
                    foreach (DataRow row2 in table2.Rows)
                    {
                        int taskID_2 = (Int32) row2[model.SqlTaskId];
                        object o = row2[model.SqlQuestion];
                        if (!(o is DBNull))
                        {
                            Question question = new Question((String) row2[model.SqlQuestion], 1,
                                                             taskID_2);
                            question.InitMultipleChoice(new string[]
                            {
                                (string) row2[model.SqlAnswer1],
                                (string) row2[model.SqlAnswer2],
                                (string) row2[model.SqlAnswer3],
                                (string) row2[model.SqlAnswer4]
                            }, (Byte) row2[model.SqlRightAnswer]);
                            catalog.Add(question);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                cmd.Dispose();
                if (model.Shuffle)
                {
                    // Do a random swap here
                    Random rand = new Random();
                    for (int i = 0; i < 100; i++)
                    {
                        int firstIndex = rand.Next(0, catalog.Count);
                        int nextIndex = rand.Next(0, catalog.Count);
                        if (firstIndex == nextIndex) continue;

                        Question temp = catalog[nextIndex];
                        catalog[nextIndex] = catalog[firstIndex];
                        catalog[firstIndex] = temp;
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.Throw(e, "Bitte überprüfen sie die Katalogkonfiguration. Die SQL Befehle sind fehlerhaft!");
                throw new Exception("catch_error");
            }
            return true;
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void CloseConnection()
        {
            connectionOpen = false;
            if (connection == null) return;
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                // Maybe there is no open connection, just break. I dont care here.
            }
        }
        
        /// <summary>
        /// Dispose pattern
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool managed)
        {
            if(managed){
                connection.Dispose(); // Just to be sure, you know.
            }
        }

        ~DB(){
            Dispose(false);
        }
    }
}
