using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MorePractice.Models;

namespace MorePractice.Controllers
{
    public class SpecializsController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();

        // GET: Specializs
        public ActionResult Index()
        {
            return View(db.Specializ.ToList());
        }

        // GET: Specializs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specializ specializ = db.Specializ.Find(id);
            if (specializ == null)
            {
                return HttpNotFound();
            }
            return View(specializ);
        }

        // GET: Specializs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specializs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Specialization")] Specializ specializ)
        {
            if (ModelState.IsValid)
            {
                specializ.ID = Guid.NewGuid();
                db.Specializ.Add(specializ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specializ);
        }

        // GET: Specializs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specializ specializ = db.Specializ.Find(id);
            if (specializ == null)
            {
                return HttpNotFound();
            }
            return View(specializ);
        }

        // POST: Specializs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Specialization")] Specializ specializ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specializ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specializ);
        }

        // GET: Specializs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specializ specializ = db.Specializ.Find(id);
            if (specializ == null)
            {
                return HttpNotFound();
            }
            return View(specializ);
        }

        // POST: Specializs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Specializ specializ = db.Specializ.Find(id);
            db.Specializ.Remove(specializ);
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
