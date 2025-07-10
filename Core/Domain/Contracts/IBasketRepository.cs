using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //Task<int> AddBasketAsync(int customerId, int productId, int quantity);
        //Task<int> UpdateBasketAsync(int customerId, int productId, int quantity);
        //Task<int> RemoveBasketAsync(int customerId, int productId);
        //Task<int> ClearBasketAsync(int customerId);
        Task<CustomerBasket?> GetBasketAsync(string Id);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket , TimeSpan? timeToLive = null);
        Task<bool> DeletBasketAsync(string Id);

    }
}
