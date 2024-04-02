using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class ConflictException : BaseException
    {
        public ConflictException() 
        {
            ErrorCode = 409;    
        }
    }
}
