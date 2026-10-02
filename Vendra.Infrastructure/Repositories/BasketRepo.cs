using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vendra.Core.Enities;
using Vendra.Core.Interfaces;

namespace Vendra.Infrastructure.Repositories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _database;
        public BasketRepo(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCustomerBasket(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket?> GetCustomerBasket(string Id)
        {
            var baskt=await _database.StringGetAsync(Id);
            return baskt.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket?>(baskt);
        }

        public async Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket customerBasket)
        {
            var created=await _database.StringSetAsync(customerBasket.Id,JsonSerializer.Serialize(customerBasket),TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetCustomerBasket(customerBasket.Id);
        }
    }
}