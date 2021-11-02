using HMS.Entities;
using HMS.Services;
using HotelManagementSystem.Areas.Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Areas.Dashboard.Controllers
{
    public class AccomodationTypeController : Controller
    {
        // GET: Dashboard/AccomodationType
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        public ActionResult Index(string searchTerm)
        {
            AccomodationTypeListingModel model = new AccomodationTypeListingModel();

            model.SearchTerm = searchTerm;

            model.AccomodationTypes = accomodationTypeService.SearchaccomodationTypes(searchTerm);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            if (ID.HasValue)
            {
                var accomodationType = accomodationTypeService.GetaccomodationTypesByID(ID.Value);
                model.ID = accomodationType.ID;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;
            }

            return PartialView("Action", model);
        }

        [HttpPost]
        public ActionResult Action(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0) 
            {
                var accomodationType = accomodationTypeService.GetaccomodationTypesByID(model.ID);

                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypeService.UpdateaccomodationTypes(accomodationType);
            }
            else 
            {
                AccomodationType accomodationType = new AccomodationType();
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypeService.SaveaccomodationTypes(accomodationType);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Types." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            var accomodationType = accomodationTypeService.GetaccomodationTypesByID(ID);
            model.ID = accomodationType.ID;

            return PartialView("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodationType = accomodationTypeService.GetaccomodationTypesByID(model.ID);

            result = accomodationTypeService.DeleteaccomodationTypes(accomodationType);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Types." };
            }

            return json;
        }
    }
}