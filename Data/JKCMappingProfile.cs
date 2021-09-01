using AutoMapper;
using JckShopping.Data.Entities;
using JckShopping.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JckShopping.Data
{
    public class JKCMappingProfile : Profile
    {
        public JKCMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o=>o.OrderId,ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap()
                .ForMember(m => m.Product, opt => opt.Ignore());
        }
    }
}
