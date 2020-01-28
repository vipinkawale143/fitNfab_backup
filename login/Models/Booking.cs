using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int Cid { get; set; }
        public int Oid { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}