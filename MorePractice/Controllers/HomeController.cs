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
    public class HomeController : Controller
    {
        //private Veterinary_clinicEntities db = new Veterinary_clinicEntities();
        private ClientPetsDbContext db = new ClientPetsDbContext();

        private ApplicationUserManager _userManager;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Описание приложения";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Связь с разработчиками";

            return View();
        }

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager)
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

        public ActionResult AdminPage()
        {
            var client = UserManager;
            return View(client);
        }

    }
}