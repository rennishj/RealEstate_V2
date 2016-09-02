using RealEstate.BLL;
using RealEstate.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RealEstate.Web.Extensions;

namespace RealEstate.Web.Controllers
{
    public class EnquiryController : Controller
    {
        public AccountLogic AccountBLL
        {
            get { return new AccountLogic(); }
        }
        
        public ActionResult Add()
        {
            EnquiryViewModel model = new EnquiryViewModel();
            return View("Add", model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(EnquiryViewModel vm)
        {
            await AccountBLL.RealEstateEnquiryCreate(vm.ToPoco());
            return Json(new JsonResponseModel(true, "Thank You for your interest ,one of our agents will contact you within two days"));
        }
    }
}