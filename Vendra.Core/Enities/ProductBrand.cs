using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;

namespace Vendra.Core.Enities
{
    public class ProductBrand:BaseEnitiy
    {
        public string Name { get; set; }
        public ICollection<ProductType> products { get; set; }
    }
}
