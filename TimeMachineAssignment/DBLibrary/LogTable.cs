using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DBLibrary
{
    public class LogTable
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        public LogTable(string connectionStr)
        {
            connection = new SqlConnection(connectionStr);
        }
        public LogTable()
        {

        }
        public int InsertLog(string logdet)
        {
            try
            {
                string sql = "insert into log values(@logdet)";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("logdet", logdet);
                int result = 0;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
