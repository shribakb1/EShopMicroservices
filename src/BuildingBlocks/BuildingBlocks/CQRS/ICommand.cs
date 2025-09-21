using MediatR;

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
