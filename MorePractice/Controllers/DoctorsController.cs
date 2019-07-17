using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MorePractice;
using MorePractice.Models;

namespace MorePractice.Controllers
{
    public class DoctorsController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();


        // GET: Doctors
        public ActionResult Index()
        {
            var doctor = db.Doctor.Include(d => d.Specializ).Include(d => d.VetClinic);
            return View(doctor.ToList());
        }

        // GET: Doctors
        public ActionResult ListForClient()
        {
            var doctor = db.Doctor.Include(d => d.Specializ).Include(d => d.VetClinic);
            return View(doctor.ToList());
        }

        // GET: Doctors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            ViewBag.ID_Special = new SelectList(db.Specializ, "ID", "Specialization");
            ViewBag.ID_Clinic = new SelectList(db.VetClinic, "ID", "Clinic");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,Fathername,ID_Clinic,ID_Special")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                doctor.ID = Guid.NewGuid();
                db.Doctor.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Special = new SelectList(db.Specializ, "ID", "Specialization", doctor.ID_Special);
            ViewBag.ID_Clinic = new SelectList(db.VetClinic, "ID", "Clinic", doctor.ID_Clinic);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Special = new SelectList(db.Specializ, "ID", "Specialization", doctor.ID_Special);
            ViewBag.ID_Clinic = new SelectList(db.VetClinic, "ID", "Clinic", doctor.ID_Clinic);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Fathername,ID_Clinic,ID_Special")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Special = new SelectList(db.Specializ, "ID", "Specialization", doctor.ID_Special);
            ViewBag.ID_Clinic = new SelectList(db.VetClinic, "ID", "Clinic", doctor.ID_Clinic);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Doctor doctor = db.Doctor.Find(id);

            var rec = db.Reception.Where(r => r.Doctor.ID == doctor.ID).ToArray();
            foreach (var r in rec)
            {
                db.Reception.Remove(r);
                db.SaveChanges();
            }

            db.Doctor.Remove(doctor);
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

        public ActionResult ExcelAdmin()
        {
            List<Doctor> doctors = db.Doctor.ToList();
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Лист1"];
            worksheet = workbook.ActiveSheet;
            //Заголовки  таблицы
            worksheet.Cells[1, 1] = "Фамилия";
            worksheet.Cells[1, 2] = "Имя";
            worksheet.Cells[1, 3] = "Отчество";
            worksheet.Cells[1, 4] = "Клиника";
            worksheet.Cells[1, 5] = "Адрес клиники";
            worksheet.Cells[1, 6] = "Специализация";

            worksheet.Name = "Врачи";

            for (int i = 0; i < doctors.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = doctors[i].Surname;
                worksheet.Cells[i + 2, 2] = doctors[i].Name;
                worksheet.Cells[i + 2, 3] = doctors[i].Fathername;
                worksheet.Cells[i + 2, 4] = doctors[i].VetClinic.Clinic;
                worksheet.Cells[i + 2, 5] = doctors[i].VetClinic.Address;
                worksheet.Cells[i + 2, 6] = doctors[i].Specializ.Specialization;

            }
            workbook.SaveAs("C:\\Users\\Аня\\Desktop\\дисциплины\\2 курс\\Практика\\Список врачей.xlsx", Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);
            app.Quit();

            return RedirectToAction("Index");
        }

        public ActionResult ExcelClient()
        {
            List<Doctor> doctors = db.Doctor.ToList();
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Лист1"];
            worksheet = workbook.ActiveSheet;
            //Заголовки  таблицы
            worksheet.Cells[1, 1] = "Фамилия";
            worksheet.Cells[1, 2] = "Имя";
            worksheet.Cells[1, 3] = "Отчество";
            worksheet.Cells[1, 4] = "Клиника";
            worksheet.Cells[1, 5] = "Адрес клиники";
            worksheet.Cells[1, 6] = "Специализация";

            worksheet.Name = "Врачи";

            for (int i = 0; i < doctors.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = doctors[i].Surname;
                worksheet.Cells[i + 2, 2] = doctors[i].Name;
                worksheet.Cells[i + 2, 3] = doctors[i].Fathername;
                worksheet.Cells[i + 2, 4] = doctors[i].VetClinic.Clinic;
                worksheet.Cells[i + 2, 5] = doctors[i].VetClinic.Address;
                worksheet.Cells[i + 2, 6] = doctors[i].Specializ.Specialization;

            }
            workbook.SaveAs("C:\\Users\\Аня\\Desktop\\дисциплины\\2 курс\\Практика\\Список врачей.xlsx", Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);
            app.Quit();

            return RedirectToAction("ListForClient");
        }


    }
}
