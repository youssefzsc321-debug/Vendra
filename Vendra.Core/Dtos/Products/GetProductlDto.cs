using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;

namespace Vendra.Core.Dtos.Products
{
    public class GetProductlDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string Image { get; set; }

        public string  BrandName { get; set; }
        public string TypeName { get; set; }


    }
}
