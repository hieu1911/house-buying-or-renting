using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() 
        {
            ErrorCode = 400;
        }
    }
}
