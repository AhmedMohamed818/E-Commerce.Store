using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using OrderProduct = Domain.Models.Product;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper , IConfiguration configuration) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);

            {
                if (basket is null) throw new BasketNotFoundException(basketId);
                foreach (var item in basket.Items)
                {
                    var product = await unitOfWork.GetRepository<OrderProduct, int>().GetAsync(item.Id);
                    if (product is null) throw new ProductNotFoundException(item.Id);
                    item.Price = product.Price;

                }
                // Check if DeliveryMetodId is null or empty since it is a string
                if (!basket.DeliveryMetodId.HasValue) throw new Exception("Invalid Delivery Method Id !!");
                var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMetodId.Value);
                if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(basket.DeliveryMetodId.Value);
                basket.ShippingPrice = deliveryMethod.Cost;
                //Amount
                var amount = (long)(basket.Items.Sum(item => item.Price * item.Quantity) + basket.ShippingPrice) * 100;








                // Initialize Stripe PaymentIntent service
                StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"]; // Replace with your actual Stripe secret key

                var service = new PaymentIntentService();
                if (string.IsNullOrEmpty(basket.PaymentIntentId))
                {
                    var options = new PaymentIntentCreateOptions
                    {
                        Amount = amount, // Convert to cents
                        Currency = "USD",
                        PaymentMethodTypes = new List<string> { "card" },
                        //Metadata = new Dictionary<string, string>
                        //{
                        //    { "BasketId", basket.Id }
                        //}
                    };
                    var intent = await service.CreateAsync(options);
                    basket.PaymentIntentId = intent.Id;
                    basket.ClientSecret = intent.ClientSecret;
                }
                else
                {
                    var options = new PaymentIntentUpdateOptions
                    {
                        Amount = amount, // Convert to cents
                    };
                    await service.UpdateAsync(basket.PaymentIntentId, options);
                }



                // Update the basket in the repository
                await basketRepository.UpdateBasketAsync(basket);
                var result = mapper.Map<BasketDto>(basket);
                return result;


            }
        }
    }
}
