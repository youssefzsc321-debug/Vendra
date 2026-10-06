using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;

namespace Vendra.Core.Enities.Order_Aggragtte
{
    public class DeliveryMethod:BaseEnitiy
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        public String ShortName { get; set; }
        public String Description { get; set; }
        public String DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
