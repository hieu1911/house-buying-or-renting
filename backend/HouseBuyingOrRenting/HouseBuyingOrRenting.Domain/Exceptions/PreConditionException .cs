using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class PreConditionException : BaseException
    {
        public PreConditionException() 
        {
            ErrorCode = 412;    
        }
    }
}
