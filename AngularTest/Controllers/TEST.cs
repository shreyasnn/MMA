using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AngularTest.DAL;

namespace AngularTest.Controllers
{
    public class TEST : Controller
    {
        private mmsdbEntities db = new mmsdbEntities();

        // GET: TEST
        public ActionResult Index()
        {
            return View(db.MMS_Beobachter.ToList());
        }

        // GET: TEST/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MMS_Beobachter mMS_Beobachter = db.MMS_Beobachter.Find(id);
            if (mMS_Beobachter == null)
            {
                return HttpNotFound();
            }
            return View(mMS_Beobachter);
        }

        // GET: TEST/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TEST/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Vorname")] MMS_Beobachter mMS_Beobachter)
        {
            if (ModelState.IsValid)
            {
                db.MMS_Beobachter.Add(mMS_Beobachter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mMS_Beobachter);
        }

        // GET: TEST/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MMS_Beobachter mMS_Beobachter = db.MMS_Beobachter.Find(id);
            if (mMS_Beobachter == null)
            {
                return HttpNotFound();
            }
            return View(mMS_Beobachter);
        }

        // POST: TEST/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Vorname")] MMS_Beobachter mMS_Beobachter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mMS_Beobachter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mMS_Beobachter);
        }

        // GET: TEST/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MMS_Beobachter mMS_Beobachter = db.MMS_Beobachter.Find(id);
            if (mMS_Beobachter == null)
            {
                return HttpNotFound();
            }
            return View(mMS_Beobachter);
        }

        // POST: TEST/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MMS_Beobachter mMS_Beobachter = db.MMS_Beobachter.Find(id);
            db.MMS_Beobachter.Remove(mMS_Beobachter);
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
    }
}
