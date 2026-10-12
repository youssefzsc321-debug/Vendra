using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;
using Vendra.Core.Enities.Order_Aggragtte;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.Specifications.Orders;

namespace Vendra.Infrastructure.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepo _basketRepo;
        private readonly IUnitOfWorkRepo _unitOfWorkRepo;
        public PaymentService(IConfiguration configuration, IBasketRepo basketRepo, IUnitOfWorkRepo unitOfWorkRepo)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWorkRepo = unitOfWorkRepo;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            var basket = await _basketRepo.GetCustomerBasket(basketId);
            if (basket == null) return null;
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWorkRepo.DeliveryMethods.GetByIdAsync(basket.DeliveryMethodId);
                shippingPrice = deliveryMethod.Cost;
            }

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWorkRepo.Products.GetByIdAsync(item.Id);
                item.Price = product.Price;
            }
            var service = new PaymentIntentService(); 
            PaymentIntent intent;
       
            if (string.IsNullOrEmpty(basket.PaymentIntentId)) 
            {
            
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price * 100) + ((long)shippingPrice * 100)),
                    Currency = "usd", 
                    AllowedPaymentMethodTypes = new List<string> { "card" },
                };

                intent = await service.CreateAsync(options);

                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions() 
                {
                    Amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price * 100) + ((long)shippingPrice * 100)) 

                };
                await service.UpdateAsync(basket.PaymentIntentId, options); 

            }

            await _basketRepo.UpdateCustomerBasket(basket);
            return basket;
        }

        public async Task<Order> UpdateOrderPaymentState(string paymentId, OrderStatus status)
        {
            var spec = new OrderWithItmesByPaymentIntentIdSpec(paymentId);
            var order = await _unitOfWorkRepo.Orders.GetByIdWithSpec(spec);
            if (order == null) return null;
            order.Status= status;
            _unitOfWorkRepo.Orders.Update(order);
            await _unitOfWorkRepo.SaveChangesAsync();
            return order;
        }
    }
}
