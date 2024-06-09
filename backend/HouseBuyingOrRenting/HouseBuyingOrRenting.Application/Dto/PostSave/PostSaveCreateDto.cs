using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class PostSaveCreateDto
    {
        public Guid Userid { get; set; }

        public Guid RealEstateId { get; set; }
    }
}
