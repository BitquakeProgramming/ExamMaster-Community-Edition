using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ExamMaster.Database
{
    public class DB:IDisposable
    {
        DataTable dataTable = new DataTable();

        public void GetDataTest()
        {
        }
        public void GetData()
        {
            string connectionString = "datasource=localhost;database=t_sbf_binnen;username=root;password=";
            string query = "select * from t_sbf_binnen";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();

            // create data adapter
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            adapt.Fill(dataTable);
            connection.Close();
            adapt.Dispose();
        }

        
        public void Dispose()
        {

        }
    }
}
