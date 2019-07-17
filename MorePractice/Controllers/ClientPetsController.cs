using Microsoft.AspNet.Identity;
using MorePractice.Models;
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

namespace MorePractice.Controllers
{
    public class ClientPetsController : Controller
    {
       // private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();
        
        private ApplicationUserManager _userManager;

        public ClientPetsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ClientPetsController()
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

        // GET: ClientPets
        public async Task<ActionResult> Index()
        {
            var client = await UserManager.FindByEmailAsync(User.Identity.Name);

            var clientPets = (from pets in db.Pets.Include(p=> p.Owner) where pets.OwnerID == client.Id select pets).ToList();
            if (clientPets == null)
                return RedirectToAction("UsOffice", "Clients");
            ViewBag.ClientId = client.Id;
            return View(clientPets);
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
