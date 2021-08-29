using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DBLibrary;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace TimeMachineAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(DateTime.Today);
            string connection = ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString;
            string path = @"C:\TRAINING\TimeMachineAssignment\";
            List<Transact> transacts = new List<Transact>();
            TimeMachineTable timeMachineTable = new TimeMachineTable(connection);
            LogTable log = new LogTable(connection);
            EmpMasterTable empMasterTable = new EmpMasterTable(connection);
            DataCollection dataCollection = new DataCollection(transacts, path, "Gate1.txt", log);
            DataCollection dataCollection2 = new DataCollection(transacts, path, "Gate2.txt", log);
            try
            {
                transacts=dataCollection.ReadData(empMasterTable, log);
                transacts = dataCollection2.ReadData(empMasterTable, log);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            List<EmployeeMaster> employees = null;
            
            DateTime date;
            int input;
            do
            {
                Console.WriteLine("Choose\n1)List of Absent\n2)List of LateComers\n3)List of late departures\n4)data collector routine\n0)quit");
                input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 0:
                        
                        break;
                    case 1:
                        Console.WriteLine("Enter date to check:");
                        date = Convert.ToDateTime(Console.ReadLine());
                        employees = timeMachineTable.GetAbsentess(date);
                        foreach (EmployeeMaster item in employees)
                            Console.WriteLine(item);
                        break;
                    case 2:
                        Console.WriteLine("Enter date to check:");
                        date = Convert.ToDateTime(Console.ReadLine());
                        employees = timeMachineTable.GetLateComers(date);
                        foreach (EmployeeMaster item in employees)
                            Console.WriteLine(item);
                        break;
                    case 3:
                        Console.WriteLine("Enter date to check:");
                        date = Convert.ToDateTime(Console.ReadLine());
                        employees = timeMachineTable.GetLateDeparture(date);
                        foreach (EmployeeMaster item in employees)
                            Console.WriteLine(item);
                        break;
                    case 4:
                        System.IO.FileInfo lastTime = new System.IO.FileInfo(@"C:\lastRunTime.txt");
                        DateTime lastRan = lastTime.LastWriteTime;
                        TimeSpan sinceLastRun = DateTime.Now - lastRan;
                        if (sinceLastRun.Hours > 23)
                        {
                            foreach (Transact item in transacts)
                            {
                                timeMachineTable.InsertMasterTable(item);
                            }
                        }
                        else
                        {
                            Console.WriteLine("run only once a day!");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;

                }
            }
            while (input != 0);
            Console.ReadLine();
        }
    }
}
