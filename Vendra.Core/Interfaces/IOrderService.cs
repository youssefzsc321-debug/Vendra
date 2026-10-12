using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Interfaces
{
    public interface IOrderService
    {
        
        Task<Order> CreateOrderAsync(string buyerEmail,string busketId ,int deliveryMethodId, Address address);
        Task<IReadOnlyList<Order>?> GetOrderesAsync(string buyerEmail);
        Task<Order?> GetOrderForUserAsync(int orderId, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>?> GetDeliveryMethodsAsync();
    }
}
