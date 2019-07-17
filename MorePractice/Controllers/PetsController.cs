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
    public class PetsController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();

        private ApplicationUserManager _userManager;

        public PetsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;

        }

        public PetsController()
        {

        }

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

        // GET: Pets
        public ActionResult Index(string searching)
        {
            var pet = db.Pets.Include(p => p.Species).Include(p => p.Owner).Where(us => us.Name.Contains(searching) || searching == null).ToList();
            return View(pet.ToList());
        }

        // GET: Pets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            ViewData["PetsReceptions"] = db.Reception.Where(item => item.ID_Pet == id);
            return View(pet);
        }

        // GET: Pets/Create
        public ActionResult Create(string clientId)
        {
            ViewBag.ID_Kinds = new SelectList(db.Kind, "ID", "PetKind");
            ViewBag.ID_Species = new SelectList(db.Species, "ID", "PetSpecies");
            ViewBag.ClientId = clientId;
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OwnerID,Name,BirthDate,IsServiced,ID_Species")] Pet pet, string clientId)
        {
            if (ModelState.IsValid)
            {
                pet.ID = Guid.NewGuid();
                var owner = db.Users.Where(c => c.Id == clientId).FirstOrDefault();
                pet.Owner = owner;
                string test = pet.Owner.Name;
                ViewBag.ClientId = clientId;
                db.Pets.Add(pet);
                db.SaveChanges();
                return RedirectToAction("Index", "ClientPets");
            }
            ViewBag.ID_Kinds = new SelectList(db.Kind, "ID", "PetKind");
            ViewBag.ID_Species = new SelectList(db.Species, "ID", "PetSpecies", pet.ID_Species);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            var client = await UserManager.FindByEmailAsync(User.Identity.Name);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            var species = db.Species.Where(s => s.Kind.ID == pet.Species.ID_Kind);
            ViewBag.ID_Species = new SelectList(species, "ID", "PetSpecies", pet.ID_Species);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,BirthDate,IsServiced,ID_Species")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                var client = await UserManager.FindByEmailAsync(User.Identity.Name);

                db.Entry(pet).State = EntityState.Modified;
                var owner = db.Users.Where(c => c.Id == client.Id).FirstOrDefault();
                pet.Owner = owner;
                db.SaveChanges();
                if (User.Identity.Name == "admin@mail.ru")
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", "ClientPets");
            }
            var species = db.Species.Where(s => s.Kind.ID == pet.Species.ID_Kind);
            ViewBag.ID_Species = new SelectList(species, "ID", "PetSpecies", pet.ID_Species);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();

            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Pet pet = db.Pets.Find(id);
            var rec = db.Reception.Where(r => r.Pet.ID == pet.ID).ToArray();
            foreach (var r in rec)
            {
                db.Reception.Remove(r);
            }
            db.Pets.Remove(pet);
            db.SaveChanges();
            return RedirectToAction("Index", "ClientPets");
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
            List<Pet> pets = db.Pets.ToList();
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Лист1"];
            worksheet = workbook.ActiveSheet;
            //Заголовки  таблицы
            worksheet.Cells[1, 1] = "Кличка";
            worksheet.Cells[1, 2] = "Дата рождения";
            worksheet.Cells[1, 3] = "Обслуживание";
            worksheet.Cells[1, 4] = "Вид";
            worksheet.Cells[1, 5] = "Порода";

            worksheet.Name = "Питомцы";

            for (int i = 0; i < pets.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = pets[i].Name;
                worksheet.Cells[i + 2, 2] = pets[i].BirthDate;
                worksheet.Cells[i + 2, 3] = pets[i].IsServiced;
                worksheet.Cells[i + 2, 4] = pets[i].Species.Kind.PetKind;
                worksheet.Cells[i + 2, 5] = pets[i].Species.PetSpecies;

            }
            workbook.SaveAs("C:\\Users\\Аня\\Desktop\\дисциплины\\2 курс\\Практика\\Список питомцев.xlsx", Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);
            app.Quit();

            return RedirectToAction("Index");
        }
    }
}
