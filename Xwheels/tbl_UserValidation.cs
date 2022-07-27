using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xwheels
{
    public class tbl_UserValidation
    {
        public int User_id { get; set; }


        [Required(ErrorMessage ="*")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Username. Cannot contain numbers or special characters")]
        public string Username { get; set; }

        


        
        [Required(ErrorMessage = "Required *")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Required *")]
        [StringLength(20, ErrorMessage = "Minimum 8 characters", MinimumLength = 8)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Required *")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Invalid number")]
        public string Contact { get; set; }


        [Display(Name ="User Status")]
        public string User_active { get; set; }
    }
    [MetadataType(typeof(tbl_UserValidation))]
    public partial class tbl_User
    {
        public string TempContact { get; set; }

        //[Required(ErrorMessage = "*")]
        //public string Username { get; set; }





        //[Required(ErrorMessage = "*")]
        //public string Email { get; set; }



        //[Required(ErrorMessage = "*")]
        //public string Password { get; set; }


        //[Required(ErrorMessage = "*")]
        //public string Contact { get; set; }

        //[Display(Name = "User Status")]
        //public string User_active { get; set; }
    }
}