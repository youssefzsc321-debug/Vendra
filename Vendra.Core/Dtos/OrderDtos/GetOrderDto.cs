using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Dtos.OrderDtos
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public string DeliveryMethodName { get; set; }

        public Address Address { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public string Status { get; set; }

        public decimal ShippingPrice { get; set; }

        public int PayMentIntentId { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total {  get; set; }
    }
    public class OrderItemDto
    {
        public int Qunatity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
}
