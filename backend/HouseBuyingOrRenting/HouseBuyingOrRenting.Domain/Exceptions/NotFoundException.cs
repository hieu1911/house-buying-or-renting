using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() 
        {
            ErrorCode = 404;
        }
    }
}
