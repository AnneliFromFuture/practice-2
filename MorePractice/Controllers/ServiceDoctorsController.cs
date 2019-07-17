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
    public class ServiceDoctorsController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();

        // GET: ServiceDoctors
        public ActionResult Index()
        {
            var serviceDoctor = db.ServiceDoctor.Include(s => s.Doctor).Include(s => s.Service);
            return View(serviceDoctor.ToList());
        }

        // GET: ServiceDoctors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDoctor serviceDoctor = db.ServiceDoctor.Find(id);
            if (serviceDoctor == null)
            {
                return HttpNotFound();
            }
            return View(serviceDoctor);
        }

        // GET: ServiceDoctors/Create
        public ActionResult Create()
        {
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "Surname");
            ViewBag.ID_Service = new SelectList(db.Service, "ID", "PetService");
            return View();
        }

        // POST: ServiceDoctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_Service,ID_Doctor")] ServiceDoctor serviceDoctor)
        {
            if (ModelState.IsValid)
            {
                serviceDoctor.ID = Guid.NewGuid();
                db.ServiceDoctor.Add(serviceDoctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "Surname", serviceDoctor.ID_Doctor);
            ViewBag.ID_Service = new SelectList(db.Service, "ID", "PetService", serviceDoctor.ID_Service);
            return View(serviceDoctor);
        }

        // GET: ServiceDoctors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDoctor serviceDoctor = db.ServiceDoctor.Find(id);
            if (serviceDoctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "Surname", serviceDoctor.ID_Doctor);
            ViewBag.ID_Service = new SelectList(db.Service, "ID", "PetService", serviceDoctor.ID_Service);
            return View(serviceDoctor);
        }

        // POST: ServiceDoctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Service,ID_Doctor")] ServiceDoctor serviceDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "Surname", serviceDoctor.ID_Doctor);
            ViewBag.ID_Service = new SelectList(db.Service, "ID", "PetService", serviceDoctor.ID_Service);
            return View(serviceDoctor);
        }

        // GET: ServiceDoctors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDoctor serviceDoctor = db.ServiceDoctor.Find(id);
            if (serviceDoctor == null)
            {
                return HttpNotFound();
            }
            return View(serviceDoctor);
        }

        // POST: ServiceDoctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ServiceDoctor serviceDoctor = db.ServiceDoctor.Find(id);
            db.ServiceDoctor.Remove(serviceDoctor);
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
