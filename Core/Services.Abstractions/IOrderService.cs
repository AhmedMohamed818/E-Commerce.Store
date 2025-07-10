using Domain.Models.OrderModels;
using Shared;
using Shared.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<OrderResultDto> GetOrdersByIdAsync(Guid Id);
        Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail);
        
        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequestDto , string userEmail);
        Task<IEnumerable<DeliverymethodDto>> GetAllDeliveryMethods();













    }
}
