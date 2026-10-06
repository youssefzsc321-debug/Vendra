using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vendra.Core.Data;
using Vendra.Core.Enities;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Seeds
{
    public class DeliveryMethodSeed
    {
        public static async Task SeedDeliveryMethod(VendraContext context)
        {
            if (!context.DeliveryMethods.Any())
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Vendra.Core", "SeedData", "delivery.json");
                if (File.Exists(filePath))
                {
                    var deliveryMethodsData = await File.ReadAllTextAsync(filePath);

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData, options);

                    if (deliveryMethod != null && deliveryMethod.Count > 0)
                    {
                        await context.DeliveryMethods.AddRangeAsync(deliveryMethod);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
