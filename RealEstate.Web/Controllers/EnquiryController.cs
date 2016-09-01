using RealEstate.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Web.Controllers
{
    public class EnquiryController : Controller
    {
        
        public ActionResult Enquiry()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Enquiry(EnquiryViewModel vm)
        { 
            return null;
        }
    }
}