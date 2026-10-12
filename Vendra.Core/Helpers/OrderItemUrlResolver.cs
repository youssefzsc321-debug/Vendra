using AutoMapper;
using Microsoft.Extensions.Configuration;
using Vendra.Core.Dtos.OrderDtos;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string?>
    {
        private readonly IConfiguration configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string? Resolve(
            OrderItem source,
            OrderItemDto destination,
            string destMember,
            ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrder.ProductImage))
            {
                return configuration["ApiUrl"] + source.ProductItemOrder.ProductImage;
            }

            return null;
        }
    }
}