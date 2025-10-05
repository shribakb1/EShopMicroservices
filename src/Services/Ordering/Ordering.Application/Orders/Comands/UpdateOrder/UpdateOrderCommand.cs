﻿using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;
using System.Windows.Input;

namespace Ordering.Application.Orders.Comands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order)
        : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool isSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        }
    }

}
