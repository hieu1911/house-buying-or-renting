
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class PostSave : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid RealEstateId { get; set; }
    }
}
