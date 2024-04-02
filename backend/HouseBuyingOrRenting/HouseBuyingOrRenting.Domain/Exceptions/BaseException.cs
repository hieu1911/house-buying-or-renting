using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public class BaseException : Exception
    {
        public int ErrorCode { get; set; }

        public dynamic? DevMessage { get; set; }

        public dynamic? UserMessage { get; set; }

        public dynamic? ErrorData { get; set; }

        public string? TraceId { get; set; }

        public string? MoreInfo { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
