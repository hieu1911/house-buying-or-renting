using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class BoardingHouse : BaseEntity
    {
        public Guid RealEstateId { get; set; }

        public RealEstate RealEstate { get; set; }

        public string? Funiture { get; set; }

        public bool SeftContained { get; set; }
    }
}
