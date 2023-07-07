using AutoMapper;
using Services.Order.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Mapping
{
    public class CustumMapping :Profile
    {
        public CustumMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.Adress, AdressDto>().ReverseMap();
        }
    }
}
