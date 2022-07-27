using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.Net;
using System.Data.Entity;

namespace Xwheels.Controllers
{
    [Authorize(Roles ="buyer")]
    public class TryController : Controller
    {
        XwheelsDBEntities db = new XwheelsDBEntities();
        

        // GET: Try
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [HttpGet][AllowAnonymous]
        public ActionResult Index(int PageNumber = 1)
        {
            
            ViewBag.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name");
            ViewBag.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name");
            ViewBag.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name");
            ViewBag.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name");
            ViewBag.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name");
            ViewBag.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits");

            
            var record = Pagination(db.tbl_Ads.Where(x=>x.Status=="Active").ToList(),PageNumber, 12, 12.0);
            
            
            return View(record);
        }
        [HttpPost][AllowAnonymous]
        public ActionResult Index([Bind(Include = "Make_id,Model_id,Category_id,Reg_city_id,Color_id,Year_id")] tbl_Ads Ad, string myQuery, string submitLists, int PageNumber = 1)
        {
            var myList = db.tbl_Ads.Where(x => x.Status == "Active").ToList();

            if (myQuery != null && myQuery != "")
            {
                myList = db.tbl_Ads.Where(x => (x.Vehicle_title.Contains(myQuery) || x.tbl_Make.Make_name.Contains(myQuery)
                            || x.tbl_Model.Model_name.Contains(myQuery) || x.tbl_Year.Year_digits.Contains(myQuery)
                            || x.tbl_Category.Category_name.Contains(myQuery) || x.tbl_Color.Color_name.Contains(myQuery))
                            && (x.Status == "Active")
                ).ToList();
            }
            if(submitLists == "Find")
            {
                myList = FindVehicle(Ad);
            }
           
                ViewBag.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name", Ad.Make_id);
                ViewBag.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name",Ad.Model_id);
                ViewBag.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name", Ad.Category_id);
                ViewBag.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name", Ad.Reg_city_id);
                ViewBag.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name", Ad.Color_id);
                ViewBag.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits", Ad.Year_id);
            
            if (myList.Count() == 0)
            {
                TempData["SearchError"] = "Oops... we didn't find anything that matches this search";
                TempData["SearchError2"] = "Try to search for something more general, change the filters or check for spelling mistakes";
            }

            var rec = Pagination(myList, PageNumber, 12, 12.0);
            return View(rec);
        }

        protected List<tbl_Ads> FindVehicle(tbl_Ads Ad)
        {
            var myList = db.tbl_Ads.Where(x => x.Status == "Active").ToList();

            //Make
            if (Ad.Make_id != null)
            {
                myList = db.tbl_Ads.Where(x => x.Make_id== Ad.Make_id).ToList();
                
            }

            //Model
            if (Ad.Model_id !=null)
            {
                myList = myList.Where(x => x.Model_id == Ad.Model_id).ToList();
            }

            //Year
            if (Ad.Year_id != null)
            {
                myList = myList.Where(x => x.Year_id == Ad.Year_id).ToList();
            }

            //Category
            if (Ad.Category_id != null)
            {
                myList = myList.Where(x => x.Category_id == Ad.Category_id).ToList();
            }

            //Register city
            if (Ad.Reg_city_id != null)
            {
                myList = myList.Where(x => x.Reg_city_id == Ad.Reg_city_id).ToList();
            }

            //Color
            if (Ad.Color_id !=null)
            {
                myList = myList.Where(x => x.Color_id == Ad.Color_id).ToList();
            }

            myList = myList.Where(x => x.Status == "Active").ToList();

            return myList;
        }

        public ActionResult GetModelList(int Make_id)
        {
            List<tbl_Model> Models = db.tbl_Model.Where(x => x.Make_id == Make_id).ToList();
            ViewBag.ModelList = new SelectList(Models, "Model_id", "Model_name");
            return PartialView("DisplayModels");
        }



        [HttpGet]
        public ActionResult Create()
        {
            tbl_Ads tbl_Ads = new tbl_Ads();
            //ViewBag.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name");
            //ViewBag.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name");

            tbl_Ads.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name");
            tbl_Ads.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name");
            tbl_Ads.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name");
            tbl_Ads.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name");
            tbl_Ads.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name");

            
            tbl_Ads.City = new SelectList(db.tbl_City.ToList(), "City_id", "City_name");
            tbl_Ads.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits");

            return View(tbl_Ads);
        }

        [HttpPost]
        public ActionResult Create(tbl_Ads Ads)
        {


            try
            {
                Ads.User_id = db.tbl_User.Where(x => x.Email == User.Identity.Name).FirstOrDefault().User_id;
                

                if (Ads.UserImageFile[0] == null)
                {
                    ViewBag.ImageOne = "Please upload minimum 1 image";
                }

                if (ModelState.IsValid && Ads.UserImageFile[0] != null)
                {
                    Ads.Status = "Inactive";
                    int count = 5;
                    for (int x = 0; x <= count; x++)
                    {
                        if (Ads.UserImageFile[x] != null)
                        {
                            SaveImage(Ads.UserImageFile[x], Ads, x);
                        }

                    }


                    db.tbl_Ads.Add(Ads);
                    db.SaveChanges();

                    ViewBag.PostAd = "Ad has been submitted. Please wait for admin approval";

                    return RedirectToAction("Index");
                }
                else
                {
                    tbl_Ads tbl_Ads = new tbl_Ads();
                    tbl_Ads.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name");
                    tbl_Ads.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name");
                    tbl_Ads.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name");
                    tbl_Ads.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name");
                    tbl_Ads.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name");

                    tbl_Ads.User = new SelectList(db.tbl_User.ToList(), "User_id", "Username");
                    tbl_Ads.City = new SelectList(db.tbl_City.ToList(), "City_id", "City_name");
                    tbl_Ads.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits");

                    ViewBag.PostAd = "Unable to Post your Ad";
                    return View(tbl_Ads);
                }
            }
            catch (Exception ex)
            {
                tbl_Ads tbl_Ads = new tbl_Ads();
                ViewBag.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name");
                ViewBag.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name");
                tbl_Ads.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name");
                tbl_Ads.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name");
                tbl_Ads.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name");

                tbl_Ads.User = new SelectList(db.tbl_User.ToList(), "User_id", "Username");
                tbl_Ads.City = new SelectList(db.tbl_City.ToList(), "City_id", "City_name");
                tbl_Ads.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits");

                ViewBag.PostAd = "Unable to Post your Ad. "+ex.Message;
                return View(tbl_Ads);
            }
           
           

            
        }

        public void SaveImage(HttpPostedFileBase File, tbl_Ads Ads, int x)
        {
            
            if (File != null)
            {
                string filename = Path.GetFileNameWithoutExtension(File.FileName);
                string ext = Path.GetExtension(File.FileName);
                filename = filename + DateTime.Now.ToString("yymmssff") + ext;

                switch (x)
                {
                    case 0:
                        {
                            //saving image to database table named tbl_Ads
                            Ads.Image_Path_1 = "~/Content/images/" + filename;

                            //Saving image to server or project
                            filename = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                            File.SaveAs(filename);
                            
                            if (Ads.Image_Path_1 == "~/Content/images/download.png")
                            {
                                Ads.Image_Path_1 = null;
                            }
                            break;
                        }
                    case 1:
                        {
                            //saving image to database table named tbl_Ads
                            Ads.Image_Path_2 = "~/Content/images/" + filename;

                            //Saving image to server or project
                            filename = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                            File.SaveAs(filename);

                            if (Ads.Image_Path_2 == "~/Content/images/download.png")
                            {
                                Ads.Image_Path_2 = null;
                            }
                            break;
                        }
                    case 2:
                        {
                            //saving image to database table named tbl_Ads
                            Ads.Image_Path_3 = "~/Content/images/" + filename;

                            //Saving image to server or project
                            filename = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                            File.SaveAs(filename);

                            if (Ads.Image_Path_3 == "~/Content/images/download.png")
                            {
                                Ads.Image_Path_3 = null;
                            }
                            break;
                        }
                    case 3:
                        {
                            //saving image to database table named tbl_Ads
                            Ads.Image_Path_4 = "~/Content/images/" + filename;

                            //Saving image to server or project
                            filename = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                            File.SaveAs(filename);

                            if (Ads.Image_Path_4 == "~/Content/images/download.png")
                            {
                                Ads.Image_Path_4 = null;
                            }
                            break;
                        }
                    case 4:
                        {
                            //saving image to database table named tbl_Ads
                            Ads.Image_Path_5 = "~/Content/images/" + filename;

                            //Saving image to server or project
                            filename = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                            File.SaveAs(filename);

                            if (Ads.Image_Path_5 == "~/Content/images/download.png")
                            {
                                Ads.Image_Path_5 = null;
                            }
                            break;
                        }
                    case 5:
                        {
                            //saving image to database table named tbl_Ads
                            Ads.Image_Path_6 = "~/Content/images/" + filename;

                            //Saving image to server or project
                            filename = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                            File.SaveAs(filename);

                            if (Ads.Image_Path_6 == "~/Content/images/download.png")
                            {
                                Ads.Image_Path_6 = null;
                            }
                            break;
                        }
                }





                
            }

            

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var tbl_Ads = db.tbl_Ads.Find(id);
           
            tbl_Ads.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name", tbl_Ads.Make_id);
            tbl_Ads.Model = new SelectList(db.tbl_Model.Where(x=>x.Make_id == tbl_Ads.Make_id).ToList(), "Model_id", "Model_name", tbl_Ads.Model_id);
            tbl_Ads.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name", tbl_Ads.Category_id);
            tbl_Ads.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name", tbl_Ads.Reg_city_id);
            tbl_Ads.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name", tbl_Ads.Color_id);

            tbl_Ads.User = new SelectList(db.tbl_User.ToList(), "User_id", "Username", tbl_Ads.User_id);
            tbl_Ads.City = new SelectList(db.tbl_City.ToList(), "City_id", "City_name", tbl_Ads.City_id);
            tbl_Ads.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits", tbl_Ads.Year_id);


            //checking images
            if(tbl_Ads.Image_Path_1 == null)
            {
                tbl_Ads.Image_Path_1 = "~/Content/images/download.png";
            }
            if (tbl_Ads.Image_Path_2 == null)
            {
                tbl_Ads.Image_Path_2 = "~/Content/images/download.png";
            }
            if (tbl_Ads.Image_Path_3 == null)
            {
                tbl_Ads.Image_Path_3 = "~/Content/images/download.png";
            }
            if (tbl_Ads.Image_Path_4 == null)
            {
                tbl_Ads.Image_Path_4 = "~/Content/images/download.png";
            }
            if (tbl_Ads.Image_Path_5 == null)
            {
                tbl_Ads.Image_Path_5 = "~/Content/images/download.png";
            }
            if (tbl_Ads.Image_Path_6 == null)
            {
                tbl_Ads.Image_Path_6 = "~/Content/images/download.png";
            }


            return View(tbl_Ads);
        }
        [HttpPost]
        public ActionResult Edit(tbl_Ads tbl_Ads)
        {
            try
            {
                tbl_Ads.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name", tbl_Ads.Make_id);
                tbl_Ads.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name", tbl_Ads.Model_id);
                tbl_Ads.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name", tbl_Ads.Category_id);
                tbl_Ads.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name", tbl_Ads.Reg_city_id);
                tbl_Ads.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name", tbl_Ads.Color_id);

                tbl_Ads.User = new SelectList(db.tbl_User.ToList(), "User_id", "Username", tbl_Ads.User_id);
                tbl_Ads.City = new SelectList(db.tbl_City.ToList(), "City_id", "City_name", tbl_Ads.City_id);
                tbl_Ads.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits", tbl_Ads.Year_id);

                if (tbl_Ads.Image_Path_1 == "~/Content/images/download.png")
                {
                    tbl_Ads.Image_Path_1 = null;
                }
                if (tbl_Ads.Image_Path_2 == "~/Content/images/download.png")
                {
                    tbl_Ads.Image_Path_2 = null;
                }
                if (tbl_Ads.Image_Path_3 == "~/Content/images/download.png")
                {
                    tbl_Ads.Image_Path_3 = null;
                }
                if (tbl_Ads.Image_Path_4 == "~/Content/images/download.png")
                {
                    tbl_Ads.Image_Path_4 = null;
                }
                if (tbl_Ads.Image_Path_5 == "~/Content/images/download.png")
                {
                    tbl_Ads.Image_Path_5 = null;
                }
                if (tbl_Ads.Image_Path_6 == "~/Content/images/download.png")
                {
                    tbl_Ads.Image_Path_6 = null;
                }
                int count = 5;
                for (int x = 0; x <= count; x++)
                {
                    if (tbl_Ads.UserImageFile[x] != null)
                    {
                        SaveImage(tbl_Ads.UserImageFile[x], tbl_Ads, x);
                    }
                }

                if (tbl_Ads.UserImageFile[0] == null)
                {
                    ViewBag.ImageOne = "Please upload minimum 1 image";
                }

                if (ModelState.IsValid )
                {
                    db.Entry(tbl_Ads).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.EditAd = "Ad submitted sucessfully";

                    return RedirectToAction("myAdd");
                }
                else
                {
                    tbl_Ads.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name", tbl_Ads.Make_id);
                    tbl_Ads.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name", tbl_Ads.Model_id);
                    tbl_Ads.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name", tbl_Ads.Category_id);
                    tbl_Ads.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name", tbl_Ads.Reg_city_id);
                    tbl_Ads.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name", tbl_Ads.Color_id);

                    tbl_Ads.User = new SelectList(db.tbl_User.ToList(), "User_id", "Username", tbl_Ads.User_id);
                    tbl_Ads.City = new SelectList(db.tbl_City.ToList(), "City_id", "City_name", tbl_Ads.City_id);
                    tbl_Ads.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits", tbl_Ads.Year_id);

                    //checking images
                    if (tbl_Ads.Image_Path_1 == null)
                    {
                        tbl_Ads.Image_Path_1 = "~/Content/images/download.png";
                    }
                    if (tbl_Ads.Image_Path_2 == null)
                    {
                        tbl_Ads.Image_Path_2 = "~/Content/images/download.png";
                    }
                    if (tbl_Ads.Image_Path_3 == null)
                    {
                        tbl_Ads.Image_Path_3 = "~/Content/images/download.png";
                    }
                    if (tbl_Ads.Image_Path_4 == null)
                    {
                        tbl_Ads.Image_Path_4 = "~/Content/images/download.png";
                    }
                    if (tbl_Ads.Image_Path_5 == null)
                    {
                        tbl_Ads.Image_Path_5 = "~/Content/images/download.png";
                    }
                    if (tbl_Ads.Image_Path_6 == null)
                    {
                        tbl_Ads.Image_Path_6 = "~/Content/images/download.png";
                    }

                    ViewBag.EditAd = "Unable to post Ad";
                    return View(tbl_Ads);
                }
            }catch(Exception ex)
            {
                tbl_Ads.Make = new SelectList(db.tbl_Make.ToList(), "Make_id", "Make_name", tbl_Ads.Make_id);
                tbl_Ads.Model = new SelectList(db.tbl_Model.ToList(), "Model_id", "Model_name", tbl_Ads.Model_id);
                tbl_Ads.Category = new SelectList(db.tbl_Category.ToList(), "Category_id", "Category_name", tbl_Ads.Category_id);
                tbl_Ads.RegCity = new SelectList(db.tbl_Reg_city.ToList(), "Reg_city_id", "Reg_city_name", tbl_Ads.Reg_city_id);
                tbl_Ads.Color = new SelectList(db.tbl_Color.ToList(), "Color_id", "Color_name", tbl_Ads.Color_id);

                tbl_Ads.User = new SelectList(db.tbl_User.ToList(), "User_id", "Username", tbl_Ads.User_id);
                tbl_Ads.City = new SelectList(db.tbl_City.ToList(), "City_id", "City_name", tbl_Ads.City_id);
                tbl_Ads.myYearr = new SelectList(db.tbl_Year.ToList(), "Year_id", "Year_digits", tbl_Ads.Year_id);

                //checking images
                if (tbl_Ads.Image_Path_1 == null)
                {
                    tbl_Ads.Image_Path_1 = "~/Content/images/download.png";
                }
                if (tbl_Ads.Image_Path_2 == null)
                {
                    tbl_Ads.Image_Path_2 = "~/Content/images/download.png";
                }
                if (tbl_Ads.Image_Path_3 == null)
                {
                    tbl_Ads.Image_Path_3 = "~/Content/images/download.png";
                }
                if (tbl_Ads.Image_Path_4 == null)
                {
                    tbl_Ads.Image_Path_4 = "~/Content/images/download.png";
                }
                if (tbl_Ads.Image_Path_5 == null)
                {
                    tbl_Ads.Image_Path_5 = "~/Content/images/download.png";
                }
                if (tbl_Ads.Image_Path_6 == null)
                {
                    tbl_Ads.Image_Path_6 = "~/Content/images/download.png";
                }

                ViewBag.EditAd = "Unable to post Ad. "+ex.Message;

                return View(tbl_Ads);
            }

        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var record = db.tbl_Ads.Find(id);
                return View(record);
            }
            return View();
        }

        public ActionResult DeleteAd(int? id)
        {
            tbl_Ads Ad = db.tbl_Ads.Find(id);

            if(Ad.Image_Path_1 != null)
            {
                DeleteImage(Ad.Image_Path_1);

            }
            if (Ad.Image_Path_2 != null)
            {
                DeleteImage(Ad.Image_Path_2);

            }
            if (Ad.Image_Path_3 != null)
            {
                DeleteImage(Ad.Image_Path_3);

            }
            if (Ad.Image_Path_4 != null)
            {
                DeleteImage(Ad.Image_Path_4);

            }
            if (Ad.Image_Path_5 != null)
            {
                DeleteImage(Ad.Image_Path_5);

            }
            if (Ad.Image_Path_6 != null)
            {
                DeleteImage(Ad.Image_Path_6);

            }

            db.tbl_Ads.Remove(Ad);
            db.SaveChanges();
            return RedirectToAction("myAdd");
        }

        public ActionResult myAdd(string myAds=null, int PageNumber=1)
        {
            int getID = db.tbl_User.Where(x => x.Email == User.Identity.Name).FirstOrDefault().User_id;
            var record = db.tbl_Ads.Where(x=>x.User_id == getID).ToList();

            if(myAds == "All" || myAds== null)
            {
                ViewBag.ListVehicle = "All Ads";
                record = Pagination(db.tbl_Ads.Where(x => x.User_id == getID).ToList(), PageNumber, 5, 5.0);

            }
            else if(myAds == "Active")
            {
                ViewBag.ListVehicle = "Approved Ads";
                record = Pagination(db.tbl_Ads.Where(x => x.Status == myAds && x.User_id==getID).ToList(), PageNumber, 5, 5.0);

            }
            else if (myAds == "Inactive")
            {
                ViewBag.ListVehicle = "Pending Ads";
                record = Pagination(db.tbl_Ads.Where(x => x.Status == myAds && x.User_id==getID).ToList(), PageNumber, 5, 5.0);

            }

            return View(record);
        }

        //Pagination Function
        protected List<tbl_Ads> Pagination(List<tbl_Ads> data, int PageNumber, int Irec, double Qrec)
        {
            

            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling((data.Count() / Qrec));

            data = data.Skip((PageNumber - 1) * Irec).Take(Irec).ToList();
            return data;
        }

        protected void DeleteImage(string Image)
        {
            string ImagePath = Server.MapPath(Image);
            FileInfo file = new FileInfo(ImagePath);
            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}