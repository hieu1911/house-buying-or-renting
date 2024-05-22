using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class DistrictDto
    {
        public Guid Id { get; set; }

        public Guid ProvinceId { get; set; }

        public Province Province { get; set; }

        public string Name { get; set; }
    }
}
