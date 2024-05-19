﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public interface IRealEstateRepository : IBaseRepository<RealEstate>
    {
        Task<List<RealEstate>> GetRealEstateForCarousel();
    }
}
