using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ImageUrlCreateDto
    {
        public Guid? RealEstateId { get; set; }

        public string Url { get; set; }
    }
}
