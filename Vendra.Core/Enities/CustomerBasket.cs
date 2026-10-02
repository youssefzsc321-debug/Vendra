using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Enities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
        public CustomerBasket()
        {
            
        }
        public CustomerBasket(string _id)
        {
            Id= _id;
        }
    }
}
