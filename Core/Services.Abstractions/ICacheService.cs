using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICacheService
    {
        Task<string?> GetCasheVakueAsync(string key);
        Task SetCasheValueAsync(string key, object value, TimeSpan duration );
    }
}
