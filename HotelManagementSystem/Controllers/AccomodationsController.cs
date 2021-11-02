using HMS.Services;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class AccomodationsController : Controller
    {
        // GET: Accomodations
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        AccomodationPackgesService accomodationPackgesService = new AccomodationPackgesService();
        AccomodationService accomodationService = new AccomodationService();
        public ActionResult Index(int accomodationTypeID, int? accomodationPackageID)
        {
            AccomodationsViewModel model = new AccomodationsViewModel();

            model.AccomodationType = accomodationTypeService.GetaccomodationTypesByID(accomodationTypeID);

            model.AccomodationPackages = accomodationPackgesService.GetAllaccomodationPackagesByAccomodationType(accomodationTypeID);

            model.SelectedAccomodationPackageID = accomodationPackageID.HasValue ? accomodationPackageID.Value : model.AccomodationPackages.First().ID;

            model.Accomodations = accomodationService.GetAllaccomodationByAccomodationPackage(model.SelectedAccomodationPackageID);
            
            return View(model);

        }

        public ActionResult Details(int accomodationPackageID)
        {
            AccomodationPackageDetailsViewModel model = new AccomodationPackageDetailsViewModel();

            model.AccomodationPackage = accomodationPackgesService.GetaccomodationPackagesByID(accomodationPackageID);

            return View(model);

        }

        public ActionResult CheckAvailability(int accomodationPackageID)
        {
            AccomodationPackageDetailsViewModel model = new AccomodationPackageDetailsViewModel();

            model.AccomodationPackage = accomodationPackgesService.GetaccomodationPackagesByID(accomodationPackageID);

            return View(model);

        }
    }
}