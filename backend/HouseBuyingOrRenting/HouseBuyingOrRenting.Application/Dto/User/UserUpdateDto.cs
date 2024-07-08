using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class UserUpdateDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }
}
