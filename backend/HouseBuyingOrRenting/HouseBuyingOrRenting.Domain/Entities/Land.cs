using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class Land : BaseEntity
    {
        public Guid RealEstateId { get; set; }

        public RealEstate RealEstate { get; set; }

        public string? LandType { get; set; }

        public bool LegalDocument { get; set; }
    }
}
