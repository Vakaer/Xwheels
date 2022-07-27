using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xwheels
{
    public class tbl_AdminValidations
    {
        
        public string Admin_name { get; set; }



        [Required(ErrorMessage ="Required*")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string Admin_email { get; set; }




        [Required(ErrorMessage = "Required*")]
        public string Admin_password { get; set; }
    }
    [MetadataType(typeof(tbl_AdminValidations))]

    public partial class tbl_Admin
    {

    }
}