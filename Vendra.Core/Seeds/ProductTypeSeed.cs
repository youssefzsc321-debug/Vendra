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
    public class ProductTypeSeed
    {
        public static async Task SeedProductType(VendraContext context)
        {
            if (!context.ProductTypes.Any())
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Vendra.Core", "SeedData", "types.json");
                if (File.Exists(filePath))
                {
                    var productTypesData = await File.ReadAllTextAsync(filePath);

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var productsTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData, options);

                    if (productsTypes != null && productsTypes.Count > 0)
                    {
                        await context.ProductTypes.AddRangeAsync(productsTypes);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
