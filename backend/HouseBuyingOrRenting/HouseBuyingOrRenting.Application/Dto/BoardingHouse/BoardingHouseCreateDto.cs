using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class BoardingHouseCreateDto : RealEstateCreateDto
    {
        public double Deposit { get; set; }

        public string? Funiture { get; set; }

        public bool SeftContained { get; set; }
    }
}
