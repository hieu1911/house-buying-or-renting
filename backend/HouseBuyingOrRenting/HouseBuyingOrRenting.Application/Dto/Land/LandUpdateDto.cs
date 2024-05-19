using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class LandUpdateDto
    {
        public Guid RealEstateId { get; set; }

        public RealEstateUpdateDto RealEstateUpdateDto { get; set; }

        public string? LandType { get; set; }

        public bool LegalDocument { get; set; }
    }
}
