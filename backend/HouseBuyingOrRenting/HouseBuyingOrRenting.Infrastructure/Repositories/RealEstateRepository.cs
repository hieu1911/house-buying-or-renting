using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class RealEstateRepository : BaseRepository<RealEstate>, IRealEstateRepository
    {
        public RealEstateRepository(MyDbContext db) : base(db, db.RealEstates)
        {
        }
    }
}
