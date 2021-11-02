using HotelManagementSystem.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Areas.Dashboard.ViewModels
{
    public class RoleListingModels
    {
        public string SearchTerm { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public Pager pager { get; set; }
    }

    public class RoleActionModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}