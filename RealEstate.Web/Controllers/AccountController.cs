using RealEstate.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Web.Controllers
{
    public class AccountController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Contact(ContactViewModel contact)
        {
            return Json("An email has been sent.Someone will get contact you in the following days");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            return Json("An email has been sent.Someone will get contact you in the following days");
        }

    }
}