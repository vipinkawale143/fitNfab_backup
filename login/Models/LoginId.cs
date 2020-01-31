using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class LoginId
    {
        [DisplayName("User Id")]
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }

        
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int id { get; set; }


    }
}