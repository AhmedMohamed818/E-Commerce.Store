using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IMapper mapper , IBasketRepository basketRepository , IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequestDto, string userEmail)
        {
            // Address
            var address = mapper.Map<Address>(orderRequestDto.ShipToAdderss);
            // order items
           var basket = await basketRepository.GetBasketAsync(orderRequestDto.BasketId);
            if (basket is null) throw new BasketNotFoundException(orderRequestDto.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id);
                if (product == null) throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem(new ProductInOrederItem(product.Id, product.Name, product.PictureUrl), product.Price , item.Quantity);
                orderItems.Add(orderItem);

            }
            // Delivery method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(orderRequestDto.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(orderRequestDto.DeliveryMethodId);
            // Subtotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            // Payment intent id
            var spec = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);
           var existsOrder= await unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);
            if (existsOrder is not null) unitOfWork.GetRepository<Order,Guid>().Delete(existsOrder);
            

            // create order logic here
            var order = new Order(userEmail,address ,orderItems , deliveryMethod , subTotal ,basket.PaymentIntentId);
            await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
           var count = await unitOfWork.SaveChangesAsync();
            if (count == 0) throw new OrderCreationBadRequestException();
            // Map the order to the result DTO
            var mappresult = mapper.Map<OrderResultDto>(order);
            return mappresult;

            }

        public async Task<IEnumerable<DeliverymethodDto>> GetAllDeliveryMethods()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<DeliverymethodDto>>(deliveryMethods);
            return result;
        }

        public async Task<OrderResultDto> GetOrdersByIdAsync(Guid Id)
        {
            var spec = new OrderSpecifications(Id);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundException(Id);
            var result = mapper.Map<OrderResultDto>(order);
            return result;
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail)
        {
            var spec = new OrderSpecifications(userEmail);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            
            var result = mapper.Map<IEnumerable<OrderResultDto>>(orders);
            return result;
        }
    }
}
