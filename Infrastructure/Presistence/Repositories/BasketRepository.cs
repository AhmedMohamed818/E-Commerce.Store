using Domain.Contracts;
using Domain.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            if (redisValue.IsNullOrEmpty) return null;
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            if (basket == null) return null;
            return basket;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var redisValue = JsonSerializer.Serialize(basket);
            var flag = await _database.StringSetAsync(basket.Id.ToString(), redisValue, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsync(basket.Id.ToString()) : null;
        }

      

        public async Task<bool> DeletBasketAsync(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
        }
    }
}
