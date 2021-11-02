using HMS.Entities;
using HMS.Services;
using HotelManagementSystem.Areas.Dashboard.ViewModels;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Areas.Dashboard.Controllers
{
    public class AccomodationController : Controller
    {
        // GET: Dashboard/Accomodation

        AccomodationService accomodationService = new AccomodationService();
        AccomodationPackgesService accomodationPackgesService = new AccomodationPackgesService();
        public ActionResult Index(string searchTerm, int? accomodationPackageID, int? page)
        {
            int recordSize = 3;
            page = page ?? 1;

            AccomodationListingModels model = new AccomodationListingModels();

            model.SearchTerm = searchTerm;
            model.AccomodationPackageID = accomodationPackageID;

            model.Accomodation = accomodationService.Searchaccomodation(searchTerm, accomodationPackageID, page.Value, recordSize);

            model.AccomodationPackage = accomodationPackgesService.GetAllaccomodationPackages();

            var totalRecords = accomodationService.SearchaccomodationCount(searchTerm, accomodationPackageID);

            model.pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationActionModel model = new AccomodationActionModel();

            if (ID.HasValue)
            {
                var accomodation = accomodationService.GetaccomodationByID(ID.Value);
                model.ID = accomodation.ID;
                model.AccomodationPackageID = accomodation.AccomodationPackageID;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
            }
            model.AccomodationPackages = accomodationPackgesService.GetAllaccomodationPackages();

            return PartialView("Action", model);
        }

        [HttpPost]
        public ActionResult Action(AccomodationActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0)
            {
                var accomodation = accomodationService.GetaccomodationByID(model.ID);

                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;
                

                result = accomodationService.Updateaccomodation(accomodation);
            }
            else
            {
                Accomodation accomodation = new Accomodation();

                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;

                result = accomodationService.Saveaccomodation(accomodation);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Packages." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationActionModel model = new AccomodationActionModel();

            var accomodation = accomodationService.GetaccomodationByID(ID);
            model.ID = accomodation.ID;

            return PartialView("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodation = accomodationService.GetaccomodationByID(model.ID);

            result = accomodationService.Deleteaccomodation(accomodation);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation." };
            }

            return json;
        }
    }
}