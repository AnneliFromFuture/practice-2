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
    public class SpeciesController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();

        // GET: Species
        public ActionResult Index()
        {
            var species = db.Species.Include(s => s.Kind);
            return View(species.ToList());
        }

        // GET: Species/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }

        // GET: Species/Create
        public ActionResult Create()
        {
            ViewBag.ID_Kind = new SelectList(db.Kind, "ID", "PetKind");
            return View();
        }

        // POST: Species/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PetSpecies,ID_Kind")] Species species)
        {
            if (ModelState.IsValid)
            {
                species.ID = Guid.NewGuid();
                db.Species.Add(species);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Kind = new SelectList(db.Kind, "ID", "PetKind", species.ID_Kind);
            return View(species);
        }

        // GET: Species/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Kind = new SelectList(db.Kind, "ID", "PetKind", species.ID_Kind);
            return View(species);
        }

        // POST: Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PetSpecies,ID_Kind")] Species species)
        {
            if (ModelState.IsValid)
            {
                db.Entry(species).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Kind = new SelectList(db.Kind, "ID", "PetKind", species.ID_Kind);
            return View(species);
        }

        // GET: Species/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }

        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Species species = db.Species.Find(id);
            db.Species.Remove(species);
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
