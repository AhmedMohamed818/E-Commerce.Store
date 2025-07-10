using AutoMapper;
using OrderAddress = Domain.Models.OrderModels.Address;
using UserAddress = Domain.Models.Identity.Address;
using Shared;
using Shared.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;

namespace Services.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.SubTotal+src.DeliveryMethod.Cost))
                .ReverseMap();
            CreateMap<DeliveryMethod, DeliverymethodDto>().ReverseMap();


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl));

            CreateMap<OrderAddress, AddressDto>().ReverseMap();
            CreateMap<UserAddress, AddressDto>().ReverseMap();

        }
    }
}
