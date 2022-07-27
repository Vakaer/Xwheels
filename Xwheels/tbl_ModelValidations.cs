using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xwheels
{
    public class tbl_ModelValidations
    {
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Name")]
        public string Model_name { get; set; }


        [Required(ErrorMessage = "Required")]
        public Nullable<int> Make_id { get; set; }
    }
    [MetadataType(typeof(tbl_ModelValidations))]

    public partial class tbl_Model
    {

    }
}