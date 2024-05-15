using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class Land : RealEstate
    {
        public string? LandType { get; set; }

        public bool LegalDocument { get; set; }
    }
}
