﻿using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class BoardingHouseProfile : Profile
    {
        public BoardingHouseProfile()
        {
            CreateMap<BoardingHouse, BoardingHouseDto>();
            CreateMap<BoardingHouseCreateDto, BoardingHouse>();
            CreateMap<BoardingHouseUpdateDto, BoardingHouse>();
        }
    }
}