using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class EmployeeMaster
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public DateTime? DTBirth { get; set; }
        public override string ToString()
        {
            return string.Format($"EmpId : {EmpID}\tEmpName : {EmpName}") ;
        }
    }
}
