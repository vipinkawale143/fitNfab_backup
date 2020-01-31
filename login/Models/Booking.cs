using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string CName { get; set; }
        public string OName { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}