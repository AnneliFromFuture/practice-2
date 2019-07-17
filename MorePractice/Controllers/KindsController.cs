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
    public class KindsController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();

        // GET: Kinds
        public ActionResult Index()
        {
            var kind = db.Kind;
            return View(kind.ToList());
        }

        // GET: Kinds/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kind.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // GET: Kinds/Create
        public ActionResult Create()
        {
            //ViewBag.ID_Class = new SelectList(db.Class, "ID", "PetClass");
            return View();
        }

        // POST: Kinds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PetKind")] Kind kind)
        {
            if (ModelState.IsValid)
            {
                kind.ID = Guid.NewGuid();
                db.Kind.Add(kind);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ID_Class = new SelectList(db.Class, "ID", "PetClass", kind.ID_Class);
            return View(kind);
        }

        // GET: Kinds/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kind.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ID_Class = new SelectList(db.Class, "ID", "PetClass", kind.ID_Class);
            return View(kind);
        }

        // POST: Kinds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PetKind")] Kind kind)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ID_Class = new SelectList(db.Class, "ID", "PetClass", kind.ID_Class);
            return View(kind);
        }

        // GET: Kinds/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kind.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: Kinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Kind kind = db.Kind.Find(id);
            db.Kind.Remove(kind);
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
