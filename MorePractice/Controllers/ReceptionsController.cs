using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MorePractice.Models;

namespace MorePractice.Controllers
{
    public class ReceptionsController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();

        private ApplicationUserManager _userManager;

        public ReceptionsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ReceptionsController()
        {    }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Receptions
        public async Task<ActionResult> Index()
        {
            var client = await UserManager.FindByEmailAsync(User.Identity.Name);
            var reception = (from receptions in db.Reception.Include(r => r.Doctor).Include(r => r.Pet).Include(r => r.Service)
                             where receptions.Pet.OwnerID == client.Id
                             select receptions).ToList();
            if (reception == null)
                return RedirectToAction("UsOffice", "Clients");
            return View(reception);
        }

        public ActionResult RecList()
        {
            var reception = db.Reception.Include(r => r.Doctor).Include(r => r.Pet).Include(r => r.Service);
            return View(reception.ToList());
        }

        // GET: Receptions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reception reception = db.Reception.Find(id);
            if (reception == null)
            {
                return HttpNotFound();
            }
            return View(reception);
        }

        // GET: Receptions/Create
        public async Task<ActionResult> Create(string clientId)
        {
            var client = await UserManager.FindByEmailAsync(User.Identity.Name);

            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "Surname");
            
            var pets = db.Pets.Where(p => p.OwnerID == client.Id);
            ViewBag.ID_Pet = new SelectList(pets, "ID", "Name");
            ViewBag.ID_Service = new SelectList(db.Service, "ID", "PetService");
            return View();
        }

        // POST: Receptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,RecTime,EmailNotification,ID_Pet,ID_Service,ID_Doctor")] Reception reception, string clientId)
        {
            if (ModelState.IsValid)
            {
                reception.ID = Guid.NewGuid();
                db.Reception.Add(reception);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var client = await UserManager.FindByEmailAsync(User.Identity.Name);
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "Surname", reception.Doctor.ID);
            var pets = db.Pets.Where(p => p.OwnerID == client.Id);
            ViewBag.ID_Pet = new SelectList(pets, "ID", "Name");
            ViewBag.ID_Service = new SelectList(db.Service, "ID", "PetService", reception.Service.ID);
            return View(reception);
        }

        // GET: Receptions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reception reception = db.Reception.Find(id);
            if (reception == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "Name", reception.ID_Doctor);
            ViewBag.ID_Pet = new SelectList(db.Pets, "ID", "Name", reception.ID_Pet);
            ViewBag.ID_Service = new SelectList(db.Service, "ID", "PetService", reception.ID_Service);
            return View(reception);
        }

        // POST: Receptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RecTime,EmailNotification,ID_Pet,ID_Service,ID_Doctor")] Reception reception)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reception).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "Name", reception.ID_Doctor);
            ViewBag.ID_Pet = new SelectList(db.Pets, "ID", "Name", reception.ID_Pet);
            ViewBag.ID_Service = new SelectList(db.Service, "ID", "PetService", reception.ID_Service);
            return View(reception);
        }

        // GET: Receptions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reception reception = db.Reception.Find(id);
            if (reception == null)
            {
                return HttpNotFound();
            }
            return View(reception);
        }

        // POST: Receptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Reception reception = db.Reception.Find(id);
            db.Reception.Remove(reception);
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
            List<Reception> receptions = db.Reception.ToList();
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Лист1"];
            worksheet = workbook.ActiveSheet;
            //Заголовки  таблицы
            worksheet.Cells[1, 1] = "Дата и время";
            worksheet.Cells[1, 2] = "Уведомление";
            worksheet.Cells[1, 3] = "Питомец";
            worksheet.Cells[1, 4] = "Услуга";
            worksheet.Cells[1, 5] = "Стоимость";
            worksheet.Cells[1, 6] = "Фамилия доктора";
            worksheet.Cells[1, 7] = "Имя";

            worksheet.Name = "Записи";

            for (int i = 0; i < receptions.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = receptions[i].RecTime;
                worksheet.Cells[i + 2, 2] = receptions[i].EmailNotification;
                worksheet.Cells[i + 2, 3] = receptions[i].Pet.Name;
                worksheet.Cells[i + 2, 4] = receptions[i].Service.PetService;
                worksheet.Cells[i + 2, 5] = receptions[i].Service.Price;
                worksheet.Cells[i + 2, 6] = receptions[i].Doctor.Name;
                worksheet.Cells[i + 2, 7] = receptions[i].Doctor.Surname;

            }
            workbook.SaveAs("C:\\Users\\Аня\\Desktop\\дисциплины\\2 курс\\Практика\\Список записей.xlsx", Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);
            app.Quit();

            return RedirectToAction("RecList");
        }
    }
}
