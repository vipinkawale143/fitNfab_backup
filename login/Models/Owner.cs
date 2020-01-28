using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class Owner
    {
        [Key]

        public int Oid { get; set; }


        [DisplayName("Email-Id")]
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Owner Name")]
        public string Name { get; set; }

        //[Range(1, 10, ErrorMessage = "Please Enter Valid Phone Number")]
        [Required(ErrorMessage = "This field is required")]
        public long Phone { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required(ErrorMessage = "This field is required")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="This field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        
        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}