using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTimeOffset? CreateDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string? CreateName { get; set; }
        
        public string? ModifiedName { get; set; }
    }
}
