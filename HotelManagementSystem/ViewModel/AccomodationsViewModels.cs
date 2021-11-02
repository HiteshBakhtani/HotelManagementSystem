﻿using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.ViewModel
{
    public class AccomodationsViewModel
    {
        public AccomodationType AccomodationType { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public int SelectedAccomodationPackageID { get; set; }
    }

    public class AccomodationPackageDetailsViewModel
    {
        public AccomodationPackage AccomodationPackage { get; set; }
    }
    
    public class CheckAvailabilityViewModel
    {
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
    }
}