using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    // Це “порожній” Command, який не повертає результат (Unit)(аналог void)
    public interface ICommand : ICommand<Unit>
    {
    }

    // Це базовий Command, який повертає результат TResponse
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
