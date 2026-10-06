using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;

namespace Vendra.Core.Enities.Order_Aggragtte
{
    public class OrderItem:BaseEnitiy
    {
        public OrderItem()
        {
            
        }
        public OrderItem(int qunatity, decimal price, ProductItemOrder productItemOrder)
        {
            Qunatity = qunatity;
            Price = price;
            ProductItemOrder = productItemOrder;
        }

        public int Qunatity { get; set; }
        public decimal Price { get; set; }
        public ProductItemOrder ProductItemOrder { get; set; }
    }
}
