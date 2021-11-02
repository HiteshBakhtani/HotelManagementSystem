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
    public class AccomodationPackagesController : Controller
    {
        // GET: Dashboard/AccomodationPakages
        AccomodationPackgesService accomodationPackgesService = new AccomodationPackgesService();
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        DashboardService dashboardService = new DashboardService();
        public ActionResult Index(string searchTerm, int? accomodationTypeID, int? page)
        {
            int recordSize = 3;
            page = page ?? 1;

            AccomodationPackgesListingModels model = new AccomodationPackgesListingModels();

            model.SearchTerm = searchTerm;
            model.AccomodationTypeID = accomodationTypeID;

            model.AccomodationPackage = accomodationPackgesService.SearchaccomodationPackages(searchTerm, accomodationTypeID, page.Value, recordSize);

            model.AccomodationTypes = accomodationTypeService.GetAllaccomodationTypes();

            var totalRecords = accomodationPackgesService.SearchaccomodationPackagesCount(searchTerm, accomodationTypeID);

            model.pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            if (ID.HasValue)
            {
                var accomodationPackages = accomodationPackgesService.GetaccomodationPackagesByID(ID.Value);
                model.ID = accomodationPackages.ID;
                model.AccomodationTypeID = accomodationPackages.AccomodationTypeID;
                model.Name = accomodationPackages.Name;
                model.NoOfRoom = accomodationPackages.NoOfRoom;
                model.FeePerNight = accomodationPackages.FeePerNight;
                model.AccomodationPackagePictures = accomodationPackgesService.GetPicturesByAccomodationPackageID(accomodationPackages.ID);
            }
            model.AccomodationTypes = accomodationTypeService.GetAllaccomodationTypes();

            return PartialView("Action", model);
        }

        [HttpPost]
        public ActionResult Action(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            List<int> pictureIDs = !string.IsNullOrEmpty(model.PictureIDs) ? model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();

            var pictures = dashboardService.GetPicturesByIDs(pictureIDs);

            if (model.ID > 0)
            {
                var accomodationPackages = accomodationPackgesService.GetaccomodationPackagesByID(model.ID);

                accomodationPackages.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackages.Name = model.Name;
                accomodationPackages.NoOfRoom = model.NoOfRoom;
                accomodationPackages.FeePerNight = model.FeePerNight;

                accomodationPackages.AccomodationPackagePictures.Clear();
                accomodationPackages.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { AccomodationPackageID = accomodationPackages.ID, PictureID = x.ID }));

                result = accomodationPackgesService.UpdateaccomodationPackages(accomodationPackages);
            }
            else
            {
                AccomodationPackage accomodationPackage = new AccomodationPackage();

                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;

                accomodationPackage.AccomodationPackagePictures = new List<AccomodationPackagePicture>();
                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { PictureID = x.ID }));

                result = accomodationPackgesService.SaveaccomodationPackages(accomodationPackage);
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
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            var accomodationPakages = accomodationPackgesService.GetaccomodationPackagesByID(ID);
            model.ID = accomodationPakages.ID;

            return PartialView("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodationType = accomodationPackgesService.GetaccomodationPackagesByID(model.ID);

            result = accomodationPackgesService.DeleteaccomodationPackages(accomodationType);

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
    }
}