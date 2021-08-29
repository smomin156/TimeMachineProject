using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DBLibrary
{
    public class TimeMachineTable
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        public TimeMachineTable(string connectionStr)
        {
            connection = new SqlConnection(connectionStr);
        }
        public List<EmployeeMaster> GetLateComers(DateTime date)
        {
            try
            {
                string sql = "select empid,empname from EMPMASTER where EMPID in(select distinct empid from TIMEMACHINE where cast(datetrans as date)=@date and" +
                    " operation='Entry' and convert(time,datetrans)>'9:30 AM')";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("date", date.Date);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                List<EmployeeMaster> employees = new List<EmployeeMaster>();
                while (reader.Read())
                {
                    EmployeeMaster emp = new EmployeeMaster();
                    emp.EmpID = Convert.ToInt32(reader["empid"]);
                    emp.EmpName = reader["empname"].ToString();
                    employees.Add(emp);
                }
                return employees;
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
        public List<EmployeeMaster> GetLateDeparture(DateTime date)
        {
            try
            {
                string sql = "select empid,empname from EMPMASTER where EMPID in(select distinct empid from TIMEMACHINE where cast(datetrans as date)=@date and" +
                    " operation='Exit' and convert(time,datetrans)>'7:30 PM')";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("date", date.Date);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                List<EmployeeMaster> employees = new List<EmployeeMaster>();
                while (reader.Read())
                {
                    EmployeeMaster emp = new EmployeeMaster();
                    emp.EmpID = Convert.ToInt32(reader["empid"]);
                    emp.EmpName = reader["empname"].ToString();
                    employees.Add(emp);
                }
                return employees;
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
            }//return list late departures
            return null;
        }
        public List<EmployeeMaster> GetAbsentess(DateTime date)
        {
            try
            {
                string sql = "select empid,empname from EMPMASTER where EMPID not in(select distinct empid from TIMEMACHINE where cast(datetrans as date)=@date)";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("date",date.Date);
                
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataReader reader= command.ExecuteReader();
                List<EmployeeMaster> employees = new List<EmployeeMaster>();
                while (reader.Read())
                {
                    EmployeeMaster emp = new EmployeeMaster();
                    emp.EmpID = Convert.ToInt32(reader["empid"]);
                    emp.EmpName = reader["empname"].ToString();
                    employees.Add(emp);
                }
                return employees;
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
       public int InsertMasterTable(Transact transact)
        {
            try
            {
                string sql = "insert into timemachine values(@gateno,@transno,@eno,@datetrans,@oper)";
                //string sql = "update timemachine set gateno=@gateno,transno=@transno,empid=@eno,transdate=@datetrans,operation=@oper";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("gateno",transact.GateNo);
                command.Parameters.AddWithValue("transno", transact.TransNo);
                command.Parameters.AddWithValue("eno", transact.EmpId);
                command.Parameters.AddWithValue("datetrans", transact.DateTrans);
                command.Parameters.AddWithValue("oper", transact.Oper);
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
