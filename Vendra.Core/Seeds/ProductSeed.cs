using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Vendra.Core.Data;
using Vendra.Core.Enities;


namespace Vendra.Core.Seeds
{
    public class ProductSeed
    {
        public static async Task SeedProductAsync(VendraContext context)
        {
            if (!context.Products.Any())
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Vendra.Core", "SeedData", "products.json");
                if (File.Exists(filePath))
                {
                    var productData = await File.ReadAllTextAsync(filePath);

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var products = JsonSerializer.Deserialize<List<Product>>(productData, options);

                    if (products != null && products.Count > 0)
                    {
                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}