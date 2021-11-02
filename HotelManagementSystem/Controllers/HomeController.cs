using HMS.Services;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
            AccomodationPackgesService accomodationPackgesService = new AccomodationPackgesService();

            model.AccomodationTypes = accomodationTypeService.GetAllaccomodationTypes();
            model.AccomodationPackages = accomodationPackgesService.GetAllaccomodationPackages();

            return View(model);
        }
    }
}