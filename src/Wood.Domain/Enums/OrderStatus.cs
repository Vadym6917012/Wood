using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wood.Domain.Enums
{
    public enum OrderStatus
    {
        New = 0,
        Processing = 1,
        Confirmed = 2,
        Delivered = 3,
        Cancelled = 4
    }
}
