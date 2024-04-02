using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class District : BaseEntity
    {
        public Guid ProvinceId { get; set; }

        public string Name { get; set; }
    }
}
