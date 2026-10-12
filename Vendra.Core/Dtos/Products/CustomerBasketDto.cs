using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;

namespace Vendra.Core.Dtos.Products
{
    public class CustomerBasketDto
    {
        [Required(ErrorMessage = "Basket Id is required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Basket must contain items")]
        public ICollection<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
