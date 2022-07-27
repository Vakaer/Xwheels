using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Xwheels.Controllers
{
    //[Authorize(Roles ="admin")]
    [AuthorizeAdmin(Roles ="admin")]
    public class AccountController : Controller
    {
        // GET: Account

        XwheelsDBEntities db = new XwheelsDBEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("login");
        }

        [HttpGet][AllowAnonymous]
        public ActionResult login()
        {


            return View();
        }
        [HttpPost][AllowAnonymous]
        public ActionResult login(tbl_Admin admin)
       {
            try
            {
                var count = db.tbl_Admin.Where(x => x.Admin_email.Equals(admin.Admin_email) && x.Admin_password.Equals(admin.Admin_password) && x.Admin_role=="admin").Count();

                if (ModelState.IsValid && count > 0)
                {
                    FormsAuthentication.SetAuthCookie(admin.Admin_email, false);
                    return RedirectToAction("Mainpage");   
                }
                else
                {
                    ViewBag.myAdminLogin = "Incorrect Email or Password!";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.myAdminLogin = "Incorrect Email or Password! "+ex.Message;
                return View();
            }
            
        }

        public ActionResult Mainpage()
        {

            ViewBag.ActiveUser = db.tbl_User.Where(x => x.User_active.Equals("Active")).Count();
            ViewBag.PendingUser = db.tbl_User.Where(x => x.User_active.Equals("Inactive")).Count();
            ViewBag.ActiveAds = db.tbl_Ads.Where(x => x.Status.Equals("Active")).Count();
            ViewBag.PendingAds = db.tbl_Ads.Where(x => x.Status.Equals("Inactive")).Count();

            var record = db.tbl_Admin.ToList();
            return View(record);
        }
        public ActionResult ListedVehicles(string myFilter, int PageNumber=1)
        {
            var record = db.tbl_Ads.ToList();

            if (myFilter == "All" || myFilter == null)
            {
                record = Pagination(db.tbl_Ads.ToList(), PageNumber);
            }
            else if (myFilter == "Active")
            {
                record = Pagination(db.tbl_Ads.Where(x => x.Status.Equals("Active")).ToList(), PageNumber);
            }
            else if (myFilter == "Inactive")
            {
                record = Pagination(db.tbl_Ads.Where(x => x.Status.Equals("Inactive")).ToList(), PageNumber);
            }
             
            return View(record);
        }
        [HttpPost]
        public ActionResult ListedVehicles(tbl_Ads ad, int request = -1, int decline = -1)
        {
            //for approve requests
            if (request >= 0)
            {
                var record = db.tbl_Ads.Find(request);
                record.Status = "Active";
                db.Entry(record).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    
                }
                

            }

            //for decline requests
            if (decline >= 0)
            {
                var record = db.tbl_Ads.Find(decline);
                record.Status = "Inactive";
                db.Entry(record).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListedVehicles");
        }


        [HttpGet]
        public ActionResult RegisterRequest(string myFilter, int PageNumber=1) {

            var record = db.tbl_User.ToList();

            if (myFilter == "All" || myFilter == null)
            {
                record = Pagination(db.tbl_User.ToList(), PageNumber); 

            }
            else if (myFilter == "Approved")
            {
                record = Pagination(db.tbl_User.Where(x => x.User_active.Equals("Active")).ToList(), PageNumber);

            }
            else if (myFilter == "Pending")
            {
                record = Pagination(db.tbl_User.Where(x => x.User_active.Equals("Inactive")).ToList(), PageNumber);

            }

            return View(record);
        }
        [HttpPost]
        public ActionResult RegisterRequest(tbl_User user, int request = -1, int decline = -1)
        {

            // request and decline are optional parameters.

            //for approve requests
            if (request >= 0)
            {
                var record = db.tbl_User.Find(request);
                record.User_active = "Active";
                db.SaveChanges();

            }

            //for decline requests
            if (decline >= 0)
            {
                var record = db.tbl_User.Find(decline);
                record.User_active = "Inactive";
                db.SaveChanges();
            }
            return RedirectToAction("RegisterRequest");
        }

        public ActionResult Details(int? id)
        {

            if (id != null)
            {
                var record = db.tbl_Ads.Find(id);
                return View(record);
            }
            return View();
        }

        public ActionResult DelVehicle(int? id)
        {
            tbl_Ads Ad = db.tbl_Ads.Find(id);
            db.tbl_Ads.Remove(Ad);
            db.SaveChanges();
            return RedirectToAction("ListedVehicles");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public List<tbl_User> Pagination(List<tbl_User> data, int PageNumber)
        {
            double drec = 5.0;
            int irec = 5;

            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling((data.Count() / drec));

            data = data.Skip((PageNumber - 1) * irec).Take(irec).ToList();
            return data;
        }

        public List<tbl_Ads> Pagination(List<tbl_Ads> data, int PageNumber)
        {
            double drec = 5.0;
            int irec = 5;

            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = Math.Ceiling((data.Count() / drec));

            data = data.Skip((PageNumber - 1) * irec).Take(irec).ToList();
            return data;
        }

    }
}