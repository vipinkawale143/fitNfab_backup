﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class OwnerPayment
    {
        public int Pid { get; set; }
        public int Oid { get; set; }
        public double Ammount { get; set; }
        public string Date { get; set; }
    }
}