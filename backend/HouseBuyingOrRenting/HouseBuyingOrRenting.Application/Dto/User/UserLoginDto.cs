using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class UserLoginDto
    {
        public string PhoneOrEmail { get; set;}

        public string Password { get; set;}

        public bool RememberLogin { get; set;}
    }
}
