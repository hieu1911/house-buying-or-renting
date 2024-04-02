using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class UnAuthorizeException : BaseException
    {
        public UnAuthorizeException() 
        {
            ErrorCode = 401;
        }
    }
}
