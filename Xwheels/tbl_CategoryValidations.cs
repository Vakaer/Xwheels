using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xwheels
{
    public class tbl_CategoryValidations
    {
        [Required(ErrorMessage ="Required")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Name")]
        public string Category_name { get; set; }
    }
    [MetadataType(typeof(tbl_CategoryValidations))]

    public partial class tbl_Category
    {

    }
}