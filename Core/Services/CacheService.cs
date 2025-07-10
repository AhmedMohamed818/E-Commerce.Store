using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICacheRepository _casheRepository) : ICacheService
    {
       

        public async Task<string?> GetCasheVakueAsync(string key)
        {
            var value = await _casheRepository.GetAsync(key);
            return value is null ? null : value;
        }
        public async Task SetCasheValueAsync(string key, object value, TimeSpan duration)
        {
            await _casheRepository.SetAsync(key, value, duration);
        }
    }
    
}
