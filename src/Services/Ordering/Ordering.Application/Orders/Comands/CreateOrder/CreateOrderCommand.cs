using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Comands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order)
        : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);

    public class CreateCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItmes should not be empty");
        }
    }


}
