using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DBLibrary
{
    public class DataCollection
    {
        List<Transact> transacts;
        string path;
        string filename;
        LogTable logtable;
        public DataCollection(List<Transact> transact1,string path1,string filename1,LogTable logTable1)
        {
            transacts = transact1;
            path = path1;
            filename = filename1;
            logtable = logTable1;
        }
        public List<Transact> ReadData(EmpMasterTable employeeMaster,LogTable logTable)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path+filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');
                        if (employeeMaster.IsEmployeeExists(Convert.ToInt32(words[2])))
                        {
                            Transact transact = new Transact();
                            transact.GateNo = Convert.ToInt32(words[0]);
                            transact.TransNo = Convert.ToInt32(words[1]);
                            transact.EmpId = Convert.ToInt32(words[2]);
                            transact.DateTrans = Convert.ToDateTime(words[3] + " " + words[4] + " " + words[5]);
                            transact.Oper = words[6];
                            transacts.Add(transact);
                        }
                        
                        else
                        {
                            int result = logTable.InsertLog(line);
                        }
                    }

                }
                return transacts;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
