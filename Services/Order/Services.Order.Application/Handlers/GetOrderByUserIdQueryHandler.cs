using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Order.Application.Dtos;
using Services.Order.Application.Mapping;
using Services.Order.Application.Queries;
using Services.Order.Infrastructure;
using ServiceShared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Handlers
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, ResponseDto<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrderByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

            if (!orders.Any())
            {
                return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(),200);
            }
            var orderDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return ResponseDto<List<OrderDto>>.Success(orderDto,200);
        }
    }
}
