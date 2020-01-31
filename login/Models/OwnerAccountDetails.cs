using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class OwnerAccountDetails
    {
        public int Oid { get; set; }
        public string BankHolderName { get; set; }
        public long AccountNumber { get; set; }
        public string BankName { get; set; }
        public string IFSC { get; set; }
        public double CurrentAmount { get; set; }

    }
}