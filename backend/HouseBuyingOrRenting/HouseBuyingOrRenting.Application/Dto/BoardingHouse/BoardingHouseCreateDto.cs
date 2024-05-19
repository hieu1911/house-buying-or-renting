using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class BoardingHouseCreateDto
    {
        public RealEstateCreateDto RealEstateCreateDto { get; set; }

        public string? Funiture { get; set; }

        public bool SeftContained { get; set; }
    }
}
