using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class BoardingHouseDto
    {
        public Guid Id { get; set; }

        public Guid RealEstateId { get; set; }

        public RealEstateDto RealEstateDto { get; set; }

        public string? Funiture { get; set; }

        public bool SeftContained { get; set; }
    }
}
