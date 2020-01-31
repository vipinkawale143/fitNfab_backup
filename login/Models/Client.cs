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

       
        [RegularExpression("^[a-zA-Z0-9@$]*", ErrorMessage = "Only Alphabets are allowed")]
        [Required(ErrorMessage = "Enter Your Name")]
        [DisplayName("Client Name")]
        public string Name { get; set; }

        [DisplayName("Email-Id")]
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [RegularExpression("^[a-zA-Z0-9@$]*", ErrorMessage = "Only Alphabets, Numbers and @, $ are allowed.")]
        [Required(ErrorMessage = "Password length should be minimum 4 and maximum 8"), MaxLength(8), MinLength(4)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required(ErrorMessage = "password not match")]
        public string ConfirmPassword { get; set; }


        [RegularExpression("^([0-9]{10})*", ErrorMessage = "Only 10 digits Numbers are allowed.")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter valid phone number")]
        public long Phone { get; set; }

        public bool Membership { get; set; }
        
        [Required(ErrorMessage = "Enter your age")]
        [Range(15, 100)]
        public int Age { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Gender { get; set; }


    }
}