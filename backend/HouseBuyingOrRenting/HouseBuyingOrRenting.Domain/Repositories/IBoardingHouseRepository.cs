﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public interface IBoardingHouseRepository : IBaseRepository<BoardingHouse>
    {
        Task<BoardingHouse> GetByRealEstateId(Guid realEstateId);
    }
}
