﻿using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class CacheRepository ( IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<string?> GetAsync(string key)
        {
           var value = await _database.StringGetAsync(key);
            return !value.IsNullOrEmpty ? value : default;

        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
           var redisvalue =  JsonSerializer.Serialize(value);
          await  _database.StringSetAsync(key, redisvalue , duration);
        }









    }
}
