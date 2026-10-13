using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Order_Aggragtte;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.Specifications.Orders;

namespace Vendra.Infrastructure.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWorkRepo _unitOfWorkRepo;
        private readonly IBasketRepo _basketRepo;
        private readonly IPaymentService _paymentService;
        public OrderService(IUnitOfWorkRepo unitOfWorkRepo, IBasketRepo basketRepo, IPaymentService paymentService)
        {
            _unitOfWorkRepo = unitOfWorkRepo;
            _basketRepo = basketRepo;
            _paymentService = paymentService;
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

            // 5.check if this basket has a previous paymentIntentId 
            var spec = new OrderWithItmesByPaymentIntentIdSpec(basket.PaymentIntentId);
            var existingOrder = await _unitOfWorkRepo.Orders.GetByIdWithSpec(spec);
            if(existingOrder!=null)
            {
                 _unitOfWorkRepo.Orders.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(busketId);
            }
            // 6.Create Order
            var order=new Order(buyerEmail,deliveryMethd,address,orderItems,subTotal,basket.PaymentIntentId);
            await _unitOfWorkRepo.Orders.AddAsync(order);

            // 7.Save To Database
            var res=await _unitOfWorkRepo.SaveChangesAsync();
            if (res <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>?> GetDeliveryMethodsAsync()
        {
            return await _unitOfWorkRepo.DeliveryMethods.GetAllAsync();
        }


        public async Task<IReadOnlyList<Order>?> GetOrderesAsync(string buyerEmail)
        {
            var spec=new OrderWithItemAndDeliveryMethodSpec(buyerEmail);
            var orders=await _unitOfWorkRepo.Orders.GetAllAsyncWithSpec(spec);
            return orders;
        }

        public async Task<Order?> GetOrderForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemAndDeliveryMethodSpec(buyerEmail, orderId);
            var order=await _unitOfWorkRepo.Orders.GetByIdWithSpec(spec);
            return order;
        }
    }
}
