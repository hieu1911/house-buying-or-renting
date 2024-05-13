using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class LandCreateDto : RealEstateCreateDto
    {
        public string? LandType { get; set; }

        public string? LegalDocument { get; set; }
    }
}
