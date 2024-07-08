using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class Payment : BaseEntity
    {
        public Guid RealEstateId { get; set; }

        public double Amount { get; set; }

        public string Transaction {  get; set; }

        public string TimeStamp { get; set; }
    }
}
