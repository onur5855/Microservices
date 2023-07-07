using MediatR;
using Services.Order.Application.Dtos;
using ServiceShared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Queries
{
    public class GetOrderByUserIdQuery:IRequest<ResponseDto<List<OrderDto>>>
    {
        public string UserId { get; set; }

    }
}
