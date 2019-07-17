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
using MorePractice;
using MorePractice.Models;

namespace MorePractice.Controllers
{
    public class ClientsController : Controller
    {
        private ClientPetsDbContext db = new ClientPetsDbContext();

        private ApplicationUserManager _userManager;

        public ClientsController()
        {
        }

        public ClientsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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

        [Authorize]
        // GET: Clients
        public async Task<ActionResult> UsOffice()
        {                      
            var client = await UserManager.FindByEmailAsync(User.Identity.Name);
            return View(client);
        }

        public ActionResult List(string searching)
        {
            var client = db.Users.Where(us => us.Surname.Contains(searching) || searching == null).ToList();
            return View(client);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Register", "Account");
        }

        // GET: Clients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Users.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = await UserManager.FindByIdAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(new EditViewModel(client));
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.ClientId);

            if (user == null)
                return HttpNotFound();

            user.UserName = model.Email;
            user.Email = model.Email;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Fathername = model.Fathername;
            user.BirthDate = model.BirthDate;
            user.PhoneNumber = model.PhoneNumber;

            var result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                if (User.Identity.Name == "admin@mail.ru")
                    return RedirectToAction("List");
                else return RedirectToAction("UsOffice");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);

            }
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Users.Find(id);

            if (client.Email == "admin@mail.ru")
            {
                ViewBag.Message = "Нельзя удалить профиль администратора";
                RedirectToAction("List");
                return View(client);
            }
            else
            {
                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(client);
            }
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Client client = db.Users.Find(id);
            if (client.Email == "admin@mail.ru")
            {
                ViewBag.Message = "Нельзя удалить профиль администратора";
                return RedirectToAction("List");
            }
            else
            {
                var pet = db.Pets.Where(p => p.OwnerID == id).ToArray();
                foreach (var p in pet)
                {
                    var rec = db.Reception.Where(r => r.Pet.ID == p.ID).ToArray();
                    foreach (var r in rec)
                    {
                        db.Reception.Remove(r);
                    }
                    db.Pets.Remove(p);
                }
                db.Users.Remove(client);
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Excel()
        {
            List<Client> clients = db.Users.ToList();
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Лист1"];
            worksheet = workbook.ActiveSheet;
            //Заголовки  таблицы
            worksheet.Cells[1, 1] = "Имя";
            worksheet.Cells[1, 2] = "Фамилия";
            worksheet.Cells[1, 3] = "Отчество";
            worksheet.Cells[1, 4] = "Дата рождения";
            worksheet.Cells[1, 5] = "Номер телефона";
            worksheet.Cells[1, 6] = "E-mail";

            worksheet.Name = "Клиенты";

            for (int i = 0; i < clients.Count; i++)
            {
                worksheet.Cells[i+2, 1] = clients[i].Name;
                worksheet.Cells[i+2, 2] = clients[i].Surname;
                worksheet.Cells[i+2, 3] = clients[i].Fathername;
                worksheet.Cells[i+2, 4] = clients[i].BirthDate;
                worksheet.Cells[i+2, 5] = clients[i].PhoneNumber;
                worksheet.Cells[i+2, 6] = clients[i].Email;

            }
            workbook.SaveAs("C:\\Users\\Аня\\Desktop\\дисциплины\\2 курс\\Практика\\Список клиентов.xlsx", Type.Missing, Type.Missing, Type.Missing, 
                Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);
            app.Quit();

            return RedirectToAction("List");
        }
    }
}
