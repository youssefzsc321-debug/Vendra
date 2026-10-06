using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Order_Aggragtte;
using Vendra.Core.Interfaces;

namespace Vendra.Infrastructure.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWorkRepo _unitOfWorkRepo;
        private readonly IBasketRepo _basketRepo;
        public OrderService(IUnitOfWorkRepo unitOfWorkRepo, IBasketRepo basketRepo)
        {
            _unitOfWorkRepo = unitOfWorkRepo;
            _basketRepo = basketRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string busketId, int deliveryMethodId, Address address)
        {
            // 1.Get Basket From Baskets Repo
            var basket = await _basketRepo.GetCustomerBasket(busketId);
            if (basket == null||!basket.Items.Any()) throw new Exception("Basket does not exist or is empty");

            // 2.Get Selected Items at Basket From Products Repo
            var orderItems=new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var product = await _unitOfWorkRepo.Products.GetByIdAsync(item.Id);
                if (product == null) throw new Exception($"Product with id {item.Id} not found");
                var productItemOrder = new ProductItemOrder(product.Id, product.Name, product.Image);
                var orderItem = new OrderItem(item.Quantity, product.Price, productItemOrder);
                orderItems.Add(orderItem);
            }
            // 3.Get Delivery Method From DeliveryMethods Repo
            var deliveryMethd=await _unitOfWorkRepo.DeliveryMethods.GetByIdAsync(deliveryMethodId);
            if(deliveryMethd==null) throw new Exception("Invalid delivery method");
            // 4.Calculate SubTotal
            var subTotal=orderItems.Sum(itme=>itme.Price*itme.Qunatity);
            
            // 5.Create Order
            var order=new Order(buyerEmail,deliveryMethd,address,orderItems,subTotal);
            await _unitOfWorkRepo.Orders.AddAsync(order);

            // 6.Save To Database
            var res=await _unitOfWorkRepo.SaveChangesAsync();
            if (res <= 0) return null;
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>?> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>?> GetOrderesAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetOrderForUser(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
