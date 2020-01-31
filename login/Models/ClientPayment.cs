using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class ClientPayment
    {
         public int Pid { get; set; }
        public int Cid { get; set; }
        public double Ammount { get; set; }
        public string Date { get; set; }
        public string TransactionId { get; set; }
    }
}