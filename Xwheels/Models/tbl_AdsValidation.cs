using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xwheels.Models
{
    public class tbl_AdsValidation
    {
        [Required]
        public Nullable<int> Make_id { get; set; }
        public Nullable<int> Model_id { get; set; }
        public Nullable<int> Category_id { get; set; }
        public Nullable<System.DateTime> year { get; set; }
        public Nullable<int> Reg_city_id { get; set; }
        public Nullable<int> Color_id { get; set; }
        public Nullable<int> City_id { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Mileage { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> User_id { get; set; }
        public string Image_path_1 { get; set; }
        public string Image_path_2 { get; set; }
        public string Image_path_3 { get; set; }
        public string Image_path_4 { get; set; }
        public string Image_path_5 { get; set; }
        public string Image_path_6 { get; set; }

        public HttpPostedFile UserImageFile { get; set; }


    }
    [MetadataType(typeof(tbl_AdsValidation))]
    public partial class tbl_Ads{
        
    
    }
}