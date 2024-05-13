﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class HouseDto : RealEstateDto
    {
        public int NumberOfBedRoom { get; set; }

        public int NumberOfToilet { get; set; }

        public int NumberOfFloor { get; set; }

        public string? Funiture { get; set; }

        public bool RedBook { get; set; }
    }
}
