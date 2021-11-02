using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomodationTypeService
    {
        public IEnumerable<AccomodationType> GetAllaccomodationTypes()
        {
            var context = new HMSContext();

            return context.AccomodationTypes.ToList();
        }

        public IEnumerable<AccomodationType> SearchaccomodationTypes(string searchTerm)
        {
            var context = new HMSContext();

            var accomodationType = context.AccomodationTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm)) 
            {
                accomodationType = accomodationType.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return accomodationType.ToList();
        }

        public AccomodationType GetaccomodationTypesByID(int ID)
        {
            var context = new HMSContext();

            return context.AccomodationTypes.Find(ID);
        }

        public bool SaveaccomodationTypes(AccomodationType accomodationType)
        {
            var context = new HMSContext();

            context.AccomodationTypes.Add(accomodationType);

            return context.SaveChanges()>0;
        }

        public bool UpdateaccomodationTypes(AccomodationType accomodationType)
        {
            var context = new HMSContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteaccomodationTypes(AccomodationType accomodationType)
        {
            var context = new HMSContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
