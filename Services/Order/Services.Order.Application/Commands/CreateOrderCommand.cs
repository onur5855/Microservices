using MediatR;
using Services.Order.Application.Dtos;
using ServiceShared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Commands
{
    public class CreateOrderCommand:IRequest<ResponseDto<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItem { get; set; }
        public AdressDto Adress { get; set; }
    }
}
