using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Xwheels
{
    public class tbl_AdsValidations
    {
        //public int Vehicle_id { get; set; }

        [Required(ErrorMessage = "Please Select Make")]
        public Nullable<int> Make_id { get; set; }


        [Required(ErrorMessage = "Please Select Model")]
        public Nullable<int> Model_id { get; set; }


        [Required(ErrorMessage = "Please Select Category")]
        public Nullable<int> Category_id { get; set; }


        [Required(ErrorMessage = "Please Select Registered City")]
        public Nullable<int> Reg_city_id { get; set; }


        [Required(ErrorMessage = "Please Select Color")]
        public Nullable<int> Color_id { get; set; }


        [Required(ErrorMessage = "Please Select Located City")]
        public Nullable<int> City_id { get; set; }

        [Required(ErrorMessage = "Please Enter Price")]
        [RegularExpression(@"^([0-9 \.\&\'\-]+)$", ErrorMessage = "Input can't be negative")]
        public Nullable<decimal> Price { get; set; }


        [Required(ErrorMessage = "Please Enter Mileage")]
        [RegularExpression(@"^([0-9 \.\&\'\-]+)$", ErrorMessage = "Input can't be negative")]
        public string Mileage { get; set; }


        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Please select Year")]
        public Nullable<int> Year_id { get; set; }

        [Required(ErrorMessage = "Please Enter Title")]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid Title")]
        //[StringLength(100, ErrorMessage = "Minimum 10 characters", MinimumLength = 10)]
        public string Vehicle_title { get; set; }



        //public Nullable<int> User_id { get; set; }
        //public string Image_Path_1 { get; set; }
        //public string Image_Path_2 { get; set; }
        //public string Image_Path_3 { get; set; }
        //public string Image_Path_4 { get; set; }
        //public string Image_Path_5 { get; set; }
        //public string Image_Path_6 { get; set; }
        //public HttpPostedFileBase[] UserImageFile { get; set; }


    }
    [MetadataType(typeof(tbl_AdsValidations))]

    public partial class tbl_Ads
    {
        public SelectList Make { get; set; }

        public SelectList Model { get; set; }

        public SelectList Category { get; set; }

        public SelectList RegCity { get; set; }

        public SelectList Color { get; set; }

        public SelectList City { get; set; }

        public SelectList User { get; set; }

        public SelectList myYearr { get; set; }

        public HttpPostedFileBase[] UserImageFile { get; set; }


    }
}