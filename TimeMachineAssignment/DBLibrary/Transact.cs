using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
   public  class Transact
    {
        public int GateNo { get; set; }
        public int TransNo { get; set; }
        public int EmpId { get; set; }
        public DateTime DateTrans { get; set; }
        public string Oper { get; set; }

        public override string ToString()
        {
            return string.Format($"GateNo : {GateNo} \t TransNo : {TransNo}\tEmpId : {EmpId}\tDateTrans : {DateTrans}\t Operation :{Oper} ");
        }
    }
}
