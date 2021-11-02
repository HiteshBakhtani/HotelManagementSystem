using HMS.Entities;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Areas.Dashboard.ViewModels
{
    public class AccomodationPackgesListingModels
    {
        public IEnumerable<AccomodationPackage> AccomodationPackage { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public int? AccomodationTypeID { get; set; }
        public Pager pager { get; set; }
    }

    public class AccomodationPackageActionModel
    {
        public int ID { get; set; }
        public int AccomodationTypeID { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public string PictureIDs { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public List<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }
    }
}