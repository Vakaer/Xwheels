using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xwheels.Models
{
    public class IndexBaseModel
    {
        public IEnumerable<Xwheels.tbl_Ads> MyProperty { get; set; }

        public List<List<tbl_Ads>> AdsList { get; set; }

        public tbl_Ads Ads { get; set; }
    }
}