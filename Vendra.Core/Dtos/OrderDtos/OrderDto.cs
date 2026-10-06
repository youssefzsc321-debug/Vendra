using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Identity;

namespace Vendra.Core.Dtos.OrderDtos
{
    public class OrderDto
    {

        [Required]
        public AddressDto Address { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]

        public int DeliveryMethodId { get; set; }

    }
}
