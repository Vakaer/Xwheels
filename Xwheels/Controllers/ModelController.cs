using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Xwheels;

namespace Xwheels.Controllers
{
    [AuthorizeAdmin(Roles ="admin")]
    public class ModelController : Controller
    {
        private XwheelsDBEntities db = new XwheelsDBEntities();

        // GET: Model
        public ActionResult Index(int PageNumber=1)
        {
            var record = Pagination(db.tbl_Model.ToList(), PageNumber);
            return View(record);
        }

       

        // GET: Model/Create
        public ActionResult Create()
        {
            ViewBag.Make_id = new SelectList(db.tbl_Make, "Make_id", "Make_name");
            return View();
        }

        // POST: Model/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Model_id,Model_name,Make_id")] tbl_Model tbl_Model)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Model.Add(tbl_Model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Make_id = new SelectList(db.tbl_Make, "Make_id", "Make_name", tbl_Model.Make_id);
            return View(tbl_Model);
        }

        // GET: Model/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Model tbl_Model = db.tbl_Model.Find(id);
            if (tbl_Model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Make_id = new SelectList(db.tbl_Make, "Make_id", "Make_name", tbl_Model.Make_id);
            return View(tbl_Model);
        }

        // POST: Model/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Model_id,Model_name,Make_id")] tbl_Model tbl_Model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Make_id = new SelectList(db.tbl_Make, "Make_id", "Make_name", tbl_Model.Make_id);
            return View(tbl_Model);
        }

       public ActionResult Delete(int? id)
        {
            
            tbl_Model mod = db.tbl_Model.Find(id);
            db.tbl_Model.Remove(mod);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Pagination function
        public List<tbl_Model> Pagination(List<tbl_Model> data, int PageNumber)
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
