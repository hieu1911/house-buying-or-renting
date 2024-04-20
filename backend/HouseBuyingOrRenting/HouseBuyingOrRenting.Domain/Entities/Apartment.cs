using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class Apartment : RealEstate
    {
        public int NumberOfBedRoom { get; set; }

        public int NumberOfToilet { get; set; }

        public int Floor { get; set; }

        public string? Status { get; set; }

        public string? LegalDocument { get; set; }
    }
}
