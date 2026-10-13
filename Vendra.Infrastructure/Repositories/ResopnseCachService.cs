using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vendra.Core.Interfaces;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Vendra.Infrastructure.Repositories
{
    public class ResopnseCachService : IResopnseCachService
    {
        private readonly IDatabase _database;
        public ResopnseCachService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task CachResponseAsync(string cachKey, object response, TimeSpan timeToLive)
        {
            if (response == null) return;

           
            var options = new JsonSerializerOptions() { PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
            var serilizedResponse=JsonSerializer.Serialize(response,options);
            await _database.StringSetAsync(cachKey, serilizedResponse, timeToLive);

        }

        public async Task<string> GetCachedRespone(string cachKey)
        {
            var cachsedResponse = await _database.StringGetAsync(cachKey);
            if (cachsedResponse.IsNullOrEmpty) return null; 

            return cachsedResponse;
        }
    }
}
