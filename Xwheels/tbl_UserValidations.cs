using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xwheels
{
    public class tbl_UserValidations
    {
        /*
            
            UserName
        */
        [Required(ErrorMessage ="Required*")]
        [Display(Name ="Name")]
        // Regular expression for validating input and error message to display
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Name")]
        public string Username { get; set; }


        /*
            
            Email
        */
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Email")]
        // Regular expression for validating input and error message to display
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]

        public string Email { get; set; }


        /*
            
            Password
        */
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /*
            
            Contact
        */
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Contact No")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Wrong mobile")]
        public string Contact { get; set; }
    }
    [MetadataType(typeof(tbl_UserValidations))]
    public partial class tbl_User
    {

    }
}