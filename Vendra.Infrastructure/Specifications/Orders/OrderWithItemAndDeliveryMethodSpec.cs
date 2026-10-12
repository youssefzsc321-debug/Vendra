using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Infrastructure.Specifications.Orders
{
    public class OrderWithItemAndDeliveryMethodSpec:BaseSpecification<Order>
    {
        public OrderWithItemAndDeliveryMethodSpec(string email,int? orderId=null) : base(o => o.BuyerEmail == email&&(orderId==null||o.Id==orderId))
        {
            AddInclude(o=>o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDesc(o => o.OrderDate);
        }
            
    }
}
