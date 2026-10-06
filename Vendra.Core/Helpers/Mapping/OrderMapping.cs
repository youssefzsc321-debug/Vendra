using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Data.Migrations;
using Vendra.Core.Dtos.Identity;
using Vendra.Core.Dtos.OrderDtos;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Helpers.Mapping
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, GetOrderDto>()
                .ForMember(dest=>dest.DeliveryMethodName,opt=>opt.MapFrom(src=>src.DeliveryMethod.ShortName))
                .ForMember(dest=>dest.ShippingPrice,opt=>opt.MapFrom(src=>src.DeliveryMethod.Cost))
                ;

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest=>dest.ProductName,opt=>opt.MapFrom(src=>src.ProductItemOrder.ProductName))
                .ForMember(dest=>dest.ProductId,opt=>opt.MapFrom(src=>src.ProductItemOrder.ProductId))
                .ForMember(dest=>dest.ProductImage,opt=>opt.MapFrom(src=>src.ProductItemOrder.ProductImage));
            CreateMap<Vendra.Core.Enities.Order_Aggragtte.Address, AddressDto>().ReverseMap();
        }
    }
}
