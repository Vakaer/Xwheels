using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Xwheels.Controllers
{
    [AuthorizeAdmin(Roles ="admin")]
    public class VehicleController : Controller
    {
        XwheelsDBEntities db = new XwheelsDBEntities();

        

        // GET: Vehicle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        [HttpPost]
        public ActionResult Create(tbl_Category cat)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.tbl_Category.Add(cat);
                    db.SaveChanges();
                    return RedirectToAction("List","Vehicle");
                }
                else
                {
                    TempData["CategoryMsg"] = "Unable to add new category";
                    return View();
                }

                
            }
            catch(Exception ex)
            {
                TempData["CategoryMsg"] = "Unable to add new category "+ex.Message;
                return View();
            }
        }

        public ActionResult List(int PageNumber=1)
        {
            var record = Pagination(db.tbl_Category.ToList(), PageNumber);
            return View(record);
        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int id=0)
        {
            return PartialView(db.tbl_Category.Find(id));
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        public ActionResult Edit(tbl_Category cat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cat).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List", "Vehicle");
                }
                else
                {
                    TempData["CategoryMsg"] = "Unable to edit category";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["CategoryMsg"] = "Unable to edit category " + ex.Message;
                return View();
            }
        }

        
        public ActionResult Delete(int? id)
        {
            tbl_Category cat = db.tbl_Category.Find(id);
            db.tbl_Category.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        //Pagination function
        public List<tbl_Category> Pagination(List<tbl_Category> data, int PageNumber)
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
