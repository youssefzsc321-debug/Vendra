using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Dtos.OrderDtos
{
    public class DeliveryMethodsDto
    {
        public String ShortName { get; set; }
        public String Description { get; set; }
        public String DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
