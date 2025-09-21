using MediatR;

namespace BuildingBlocks.CQRS
{
    // Базовий інтерфейс для Query (читальний запит)
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull   
    {
    }
}
