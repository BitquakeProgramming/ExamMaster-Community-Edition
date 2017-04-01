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

namespace ExamMaster.Database
{
    public class DB : IDisposable
    {
        private MySqlConnection connection;
        public void GetDataTest()
        {
        }
        public void OpenConnection()
        {
            string connectionString = "datasource=localhost;database=binnenschifffahrt;username=root;password=";
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        private QuestionCatalog MakeBinnenCatalog()
        {
            return new QuestionCatalog("Binnenschifffahrt");
        }

        public bool FillCatalogBinnen(ref QuestionCatalog catalog, CatalogModel model, int varId)
        {
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
                    string query2 = "SELECT * FROM " + model.SQLTaskDbName + " WHERE " + model.SQL_TaskID + " = " +
                                    taskID;
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    DataTable table2 = new DataTable();
                    table2.Load(reader2);
                    foreach (DataRow row2 in table2.Rows)
                    {
                        int taskID_2 = (Int32) row2[model.SQL_TaskID];
                        object o = row2[model.SQL_Question];
                        if (!(o is DBNull))
                        {
                            Question question = new Question((String) row2[model.SQL_Question], 1,
                                                             taskID_2);
                            question.InitMultipleChoice(new string[]
                            {
                                (string) row2[model.SQL_Answer1],
                                (string) row2[model.SQL_Answer2],
                                (string) row2[model.SQL_Answer3],
                                (string) row2[model.SQL_Answer4]
                            }, (Byte) row2[model.SQL_RightAnswer]);
                            catalog.Add(question);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                cmd.Dispose();
            return true;
        }

        public void CloseConnection()
        {
            if (connection == null) return;
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                
            }
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
