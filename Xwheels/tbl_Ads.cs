//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xwheels
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Ads
    {
        public int Vehicle_id { get; set; }
        public Nullable<int> Make_id { get; set; }
        public Nullable<int> Model_id { get; set; }
        public Nullable<int> Category_id { get; set; }
        public Nullable<int> Reg_city_id { get; set; }
        public Nullable<int> Color_id { get; set; }
        public Nullable<int> City_id { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Mileage { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Nullable<int> User_id { get; set; }
        public string Image_Path_1 { get; set; }
        public string Image_Path_2 { get; set; }
        public string Image_Path_3 { get; set; }
        public string Image_Path_4 { get; set; }
        public string Image_Path_5 { get; set; }
        public string Image_Path_6 { get; set; }
        public Nullable<int> Year_id { get; set; }
        public string Vehicle_title { get; set; }
    
        public virtual tbl_Category tbl_Category { get; set; }
        public virtual tbl_City tbl_City { get; set; }
        public virtual tbl_Color tbl_Color { get; set; }
        public virtual tbl_Make tbl_Make { get; set; }
        public virtual tbl_Model tbl_Model { get; set; }
        public virtual tbl_Reg_city tbl_Reg_city { get; set; }
        public virtual tbl_User tbl_User { get; set; }
        public virtual tbl_Year tbl_Year { get; set; }
    }
}
