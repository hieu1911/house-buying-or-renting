using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class RealEstateUpdateDto
    {
        public Guid OwnerId { get; set; }

        public Guid? DistrictId { get; set; }

        public string? Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longtitude { get; set; }

        public double Area { get; set; }

        public string? Title { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public string? Feature { get; set; }

        public PostType Type { get; set; }

        public RealEstateType RealEstateType { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string? CreatedName { get; set; }

        public string? ModifiedName { get; set; }

        public List<ImageUrl> ImageUrl { get; set; }
    }
}
