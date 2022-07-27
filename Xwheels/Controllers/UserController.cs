using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Xwheels.Model;
namespace Xwheels.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        XwheelsDBEntities db = new XwheelsDBEntities();
        // GET: User

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(tbl_User user)
        {
            try {

                var email = db.tbl_User.Where(x => x.Email.Equals(user.Email)).Count();
                var num = db.tbl_User.Where(x => x.Contact.Equals(user.Contact)).Count();

                if(email != 0)
                {
                    ViewBag.email = "Email already exists";
                }
                if (num != 0)
                {
                    ViewBag.num = "Contact number already exists";
                }

                if (ModelState.IsValid && email == 0 && num == 0)
                {
                    user.User_active = "Inactive";
                    user.User_role = "buyer";
                    db.tbl_User.Add(user);
                    db.SaveChanges();

                    //Custom message on VIEW
                    ViewBag.myRegister = "We have received your Registration request. Please wait for Admin approval ";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    
                    return View();
                }
            }
            catch (Exception ex){

                ViewBag.myRegister = "Unable to register. "+ex.Message;
                return View();
            }

            
           
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_User myUser)
        {
            try
            {

                

                if (myUser.Email != null && myUser.Password !=null)
                {
                    var users = db.tbl_User.Where(x => x.Email == myUser.Email && x.Password == myUser.Password && x.User_active =="Active").Count();

                    if (users > 0)
                    {
                        ViewBag.myLogin = "Logged in successfully";
                        FormsAuthentication.SetAuthCookie(myUser.Email, false);

                        return RedirectToAction("Index","Try");
                    }
                    else if(db.tbl_User.Where(x=> x.Email==myUser.Email && x.User_active=="Inactive").Count() > 0)
                    {
                        ViewBag.myLogin = "Please wait for Admin approval";
                        return View();
                    }
                    else if(db.tbl_User.Where(x=> x.Email==myUser.Email).Count() == 0)
                    {
                        ViewBag.myLogin = "User doesn't exist!";
                        return View();
                    }
                    else
                    {
                        ViewBag.myLogin = "Incorrect Email or Password!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.myLogin = "Incorrect Email or Password!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.myLogin = "Login failed "+ex.Message;
                return View();
            }

        }

        

        public ActionResult EditProfile()
        {
            int getID = db.tbl_User.Where(x => x.Email == User.Identity.Name).FirstOrDefault().User_id;
            var user = db.tbl_User.Find(getID);
            string temp = user.Contact;
            user.TempContact = temp;

            return View(user);
        }
        [HttpPost]
        public ActionResult EditProfile(tbl_User objUser)
        {
            try
            {
                var myEmail = 0;
                var myNum = 0;

                if (User.Identity.Name != objUser.Email)
                {
                    myEmail = db.tbl_User.Where(x => x.Email.Equals(objUser.Email)).Count();
                }
                if(objUser.TempContact != objUser.Contact)
                {
                    myNum = db.tbl_User.Where(x => x.Contact == objUser.Contact).Count();
                }

                if (myEmail != 0)
                {
                    ViewBag.email = "Email already exists";
                }
                if (myNum != 0)
                {
                    ViewBag.num = "Contact number already exists";
                }

                if (ModelState.IsValid && myEmail == 0 && myNum == 0)
                {
                    db.Entry(objUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(objUser.Email, false);
                    ViewBag.EditProfile = "Your Profile has been updated successfully!";
                    return RedirectToAction("EditProfile");
                }
                else
                {
                    ViewBag.EditProfile = "Unable to update your profile";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.EditProfile = "Unable to update your profile. "+ex.Message;
                return View();
            }

         }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return Redirect("Login");
        }
    }
}