﻿using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface ILandService : IBaseService<Land, LandDto, LandCreateDto, LandUpdateDto>
    {
        Task<LandDto> GetByRealEstateId(Guid realEstateId);
    }
}
