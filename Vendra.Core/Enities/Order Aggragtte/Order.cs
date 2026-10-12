using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;

namespace Vendra.Core.Enities.Order_Aggragtte
{
    public class Order : BaseEnitiy
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, DeliveryMethod deliveryMethod, Address address, ICollection<OrderItem> orderItems, decimal subTotal, string PayMentIntentId)
        {
            BuyerEmail = buyerEmail;
            DeliveryMethod = deliveryMethod;
            Address = address;
            OrderItems = orderItems;
            SubTotal = subTotal;
            this.PayMentIntentId = PayMentIntentId; 
        }

        public string BuyerEmail { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        public Address Address { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string PayMentIntentId { get; set; }

        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;




    }
}
