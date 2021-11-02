using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomodationPackgesService
    {
        public IEnumerable<AccomodationPackage> GetAllaccomodationPackages()
        {
            var context = new HMSContext();

            return context.AccomodationPackages.ToList();
        }

        public IEnumerable<AccomodationPackage> GetAllaccomodationPackagesByAccomodationType(int accomodationTypeID)
        {
            var context = new HMSContext();

            return context.AccomodationPackages.Where(x=>x.AccomodationTypeID== accomodationTypeID).ToList();
        }

        public IEnumerable<AccomodationPackage> SearchaccomodationPackages(string searchTerm, int? accomodationTypeID, int page, int recordSize)
        {
            var context = new HMSContext();

            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeID == accomodationTypeID.Value);
            }

            var skip = (page - 1) * recordSize;

            return accomodationPackages.OrderBy(x=>x.AccomodationTypeID).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchaccomodationPackagesCount(string searchTerm, int? accomodationTypeID)
        {
            var context = new HMSContext();

            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeID == accomodationTypeID.Value);
            }

            return accomodationPackages.Count();
        }

        public AccomodationPackage GetaccomodationPackagesByID(int ID)
        {
            var context = new HMSContext();
            
            return context.AccomodationPackages.Find(ID);
             
        }

        public bool SaveaccomodationPackages(AccomodationPackage accomodationPackages)
        {
            var context = new HMSContext();

            context.AccomodationPackages.Add(accomodationPackages);

            return context.SaveChanges() > 0;
        }

        public bool UpdateaccomodationPackages(AccomodationPackage accomodationPackages)
        {
            var context = new HMSContext();

            var existingAccomodationPackage = context.AccomodationPackages.Find(accomodationPackages.ID);

            context.AccomodationPackagePictures.RemoveRange(existingAccomodationPackage.AccomodationPackagePictures);

            //context.Entry(accomodationPackages).State = System.Data.Entity.EntityState.Modified;
            context.Entry(existingAccomodationPackage).CurrentValues.SetValues(accomodationPackages);

            context.AccomodationPackagePictures.AddRange(accomodationPackages.AccomodationPackagePictures);

            return context.SaveChanges() > 0;
        }

        public bool DeleteaccomodationPackages(AccomodationPackage accomodationPackages)
        {
            var context = new HMSContext();

            var existingAccomodationPackage = context.AccomodationPackages.Find(accomodationPackages.ID);

            //context.Entry(accomodationPackages).State = System.Data.Entity.EntityState.Deleted;
            context.AccomodationPackagePictures.RemoveRange(existingAccomodationPackage.AccomodationPackagePictures);

            context.Entry(existingAccomodationPackage).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }

        public List<AccomodationPackagePicture> GetPicturesByAccomodationPackageID(int accomodationPackageID)
        {
            var context = new HMSContext();

            return context.AccomodationPackages.Find(accomodationPackageID).AccomodationPackagePictures.ToList();
        }
    }
}
