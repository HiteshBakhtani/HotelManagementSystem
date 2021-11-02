using HMS.Entities;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Areas.Dashboard.ViewModels
{
    public class AccomodationListingModels
    {
        public IEnumerable<Accomodation> Accomodation { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackage { get; set; }
        public int? AccomodationPackageID { get; set; }
        public Pager pager { get; set; }
    }

    public class AccomodationActionModel
    {
        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
}