using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;

namespace Vendra.Core.Enities
{
    public class Product:BaseEnitiy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string Image { get; set; }
        public string? ImageThumnail { get; set; }

        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
