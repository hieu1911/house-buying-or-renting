using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class ImageUrl : BaseEntity
    {
        public Guid RealEstateId { get; set; }

        public string Url { get; set; }
    }
}
