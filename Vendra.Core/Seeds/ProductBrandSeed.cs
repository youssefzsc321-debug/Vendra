using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vendra.Core.Data;
using Vendra.Core.Enities;

namespace Vendra.Core.Seeds
{
    public class ProductBrandSeed
    {
        public static async Task SeedProductBrand(VendraContext context)
        {
            if(!context.productBrands.Any())
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Vendra.Core", "SeedData", "brands.json");
                if (File.Exists(filePath))
                {
                    var productBrandData = await File.ReadAllTextAsync(filePath);

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var productsBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandData, options);

                    if (productsBrands != null && productsBrands.Count > 0)
                    {
                        await context.productBrands.AddRangeAsync(productsBrands);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
