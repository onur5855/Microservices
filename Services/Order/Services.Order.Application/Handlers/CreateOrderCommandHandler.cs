using MediatR;
using Services.Order.Application.Commands;
using Services.Order.Application.Dtos;
using Services.Order.Domain.OrderAggregate;
using Services.Order.Infrastructure;
using ServiceShared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseDto< CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAdress = new Adress(
                request.Adress.Province,
                request.Adress.District,
                request.Adress.Street,
                request.Adress.ZipCode,
                request.Adress.Line);
            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId,newAdress);
            request.OrderItem.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId=newOrder.Id},200);
        }
    }
}
