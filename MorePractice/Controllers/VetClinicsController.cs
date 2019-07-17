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
    public class VetClinicsController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();

        // GET: VetClinics
        public ActionResult Index()
        {
            return View(db.VetClinic.ToList());
        }

        // GET: VetClinics/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VetClinic vetClinic = db.VetClinic.Find(id);
            if (vetClinic == null)
            {
                return HttpNotFound();
            }
            return View(vetClinic);
        }

        // GET: VetClinics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VetClinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Clinic,Address")] VetClinic vetClinic)
        {
            if (ModelState.IsValid)
            {
                vetClinic.ID = Guid.NewGuid();
                db.VetClinic.Add(vetClinic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vetClinic);
        }

        // GET: VetClinics/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VetClinic vetClinic = db.VetClinic.Find(id);
            if (vetClinic == null)
            {
                return HttpNotFound();
            }
            return View(vetClinic);
        }

        // POST: VetClinics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Clinic,Address")] VetClinic vetClinic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vetClinic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vetClinic);
        }

        // GET: VetClinics/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VetClinic vetClinic = db.VetClinic.Find(id);
            if (vetClinic == null)
            {
                return HttpNotFound();
            }
            return View(vetClinic);
        }

        // POST: VetClinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VetClinic vetClinic = db.VetClinic.Find(id);
            db.VetClinic.Remove(vetClinic);
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

        public ActionResult Excel()
        {
            List<VetClinic> clinics = db.VetClinic.ToList();
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Лист1"];
            worksheet = workbook.ActiveSheet;
            //Заголовки  таблицы
            worksheet.Cells[1, 1] = "Название";
            worksheet.Cells[1, 2] = "Адрес";

            worksheet.Name = "Вет клиники";

            for (int i = 0; i < clinics.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = clinics[i].Clinic;
                worksheet.Cells[i + 2, 2] = clinics[i].Address;
            }
            workbook.SaveAs("C:\\Users\\Аня\\Desktop\\дисциплины\\2 курс\\Практика\\Список вет клиник.xlsx", Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);
            app.Quit();

            return RedirectToAction("Index");
        }

    }
}
