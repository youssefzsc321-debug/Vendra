using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;

namespace Vendra.Core.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, GetProductlDto, string?>
    {
        private readonly IConfiguration configuration;
        public ProductUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string? Resolve(Product source, GetProductlDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Image))
            {
                return configuration["ApiUrl"] +source.Image;
            }
            return null;
        }
    }
}
