using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class MessageCreateDto
    {
        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public string Content { get; set; }
    }
}
