using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Infrastructure.Specifications.Orders
{
    public class OrderWithItmesByPaymentIntentIdSpec:BaseSpecification<Order>
    {
        public OrderWithItmesByPaymentIntentIdSpec(string paymentIntentId) : base(o=>o.PayMentIntentId== paymentIntentId)
        {
            AddInclude(o => o.OrderItems);
            
        }
    }
}
