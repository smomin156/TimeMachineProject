using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DBLibrary
{
    public class EmpMasterTable
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public EmpMasterTable(string connectionStr)
        {
            connection = new SqlConnection(connectionStr);
        }
        public EmpMasterTable()
        {
            
        }
        public bool IsEmployeeExists(int eno)
        {
            try
            {
                string sql = "select * from empmaster where empid=@eno";
                command= new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("eno", eno);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader = command.ExecuteReader();
                if (reader.HasRows==false)
                {
                    return false;
                }
                return true;
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
