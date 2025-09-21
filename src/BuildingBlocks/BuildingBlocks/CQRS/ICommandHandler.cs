using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommandHnalder<in TCommand>
       : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
        
    }

    public interface ICommandHandler<in TCommand, TResponce>
        : IRequestHandler<TCommand, TResponce>
        where TCommand : ICommand<TResponce>
        where TResponce : notnull
    {
    }
}
