using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Enities.Order_Aggragtte
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending=0,
        [EnumMember(Value = "Payment Received")]
        PaymentReceived=1,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed=2
    }
}
