using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class Client
    {
        [key]
        
        public int Cid { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [DisplayName("Client Name")]
        public string Name { get; set; }

        [DisplayName("Email-Id")]
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required(ErrorMessage = "This field is required")]
        public string ConfirmPassword { get; set; }

       // [Range(1, 10, ErrorMessage = "Please Enter Valid Phone Number")]
        [Required(ErrorMessage = "This field is required")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public bool Membership { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Gender { get; set; }


    }
}