using HMS.Entities;
using HMS.Services;
using HotelManagementSystem.Areas.Dashboard.ViewModels;
using HotelManagementSystem.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        // GET: Dashboard/Roles
        // GET: Dashboard/Users
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRoleManager _roleManager;

        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public HMSRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index(string searchTerm, int? page)
        {
            int recordSize = 3;
            page = page ?? 1;

            RoleListingModels model = new RoleListingModels();

            model.SearchTerm = searchTerm;

            model.Roles = SearchRole(searchTerm, page.Value, recordSize);

            var totalRecords = SearchRoleCount(searchTerm);

            model.pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        public IEnumerable<IdentityRole> SearchRole(string searchTerm, int page, int recordSize)
        {

            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var skip = (page - 1) * recordSize;

            return roles.OrderBy(x => x.Name).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchRoleCount(string searchTerm)
        {

            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return roles.Count();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            RoleActionModel model = new RoleActionModel();

            if (!string.IsNullOrEmpty(ID))
            {
                var role = await RoleManager.FindByIdAsync(ID);

                model.ID = role.Id;
                model.Name = role.Name;
            }
            //model.Roles = accomodationPackgesService.GetAllaccomodationPackages();

            return PartialView("Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RoleActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID))
            {
                var role = await RoleManager.FindByIdAsync(model.ID);

                role.Name = model.Name;

                result = await RoleManager.UpdateAsync(role);
            }
            else
            {
                var role = new IdentityRole();

                role.Name = model.Name;

                result = await RoleManager.CreateAsync(role);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            RoleActionModel model = new RoleActionModel();

            var role = await RoleManager.FindByIdAsync(ID);

            model.ID = role.Id;

            return PartialView("Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UserActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID))
            {
                var role = await RoleManager.FindByIdAsync(model.ID);

                result = await RoleManager.DeleteAsync(role);

                json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid role." };
            }

            return json;
        }
    }
}