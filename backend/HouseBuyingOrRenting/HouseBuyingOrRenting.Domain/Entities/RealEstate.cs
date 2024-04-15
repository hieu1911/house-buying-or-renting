using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class RealEstate : BaseEntity
    {
        public Guid OwnerId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid? DistrictId { get; set; }

        public string? Address { get; set; }

        public string Title { get; set; }

        public string? Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double Deposit { get; set; }
    }
}
