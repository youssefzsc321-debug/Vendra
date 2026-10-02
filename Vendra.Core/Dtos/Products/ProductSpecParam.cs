using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Dtos.Products
{
    public class ProductSpecParam
    {
        public string? Sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _take = 10;

        public int Take 
        { 
            get => _take; 
            set => _take = (value > MaxPageSize ? MaxPageSize : value);
        }

        private string? _search;

        public string? Search
        {
            get => _search; 
            set => _search = value.ToLower(); 
        }

    


    }
}
