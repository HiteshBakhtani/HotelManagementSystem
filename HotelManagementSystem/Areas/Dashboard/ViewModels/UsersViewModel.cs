using HMS.Entities;
using HotelManagementSystem.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Areas.Dashboard.ViewModels
{
        public class UserListingModels
        {
            public IEnumerable<HMSUser> Users { get; set; }
            public string SearchTerm { get; set; }
            public IEnumerable<IdentityRole> Roles { get; set; }
            public string RoleID { get; set; }
            public Pager pager { get; set; }
        }

        public class UserActionModel
        {
            public string ID { get; set; }
            //public string RoleID { get; set; }
            //public IdentityRole Role { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            //public IEnumerable<IdentityRole> Roles { get; set; }
    }

        public class UserRolesModel
        { 
            public string UserID { get; set; }
            public IEnumerable<IdentityRole> Roles { get; set; }
            public IEnumerable<IdentityRole> UserRoles { get; set; }

    }
}