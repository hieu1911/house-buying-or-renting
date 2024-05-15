using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ApartmentUpdateDto : RealEstateUpdateDto
    {
        public int NumberOfBedRoom { get; set; }

        public int NumberOfToilet { get; set; }

        public int Floor { get; set; }

        public string? Status { get; set; }

        public bool LegalDocument { get; set; }
    }
}
