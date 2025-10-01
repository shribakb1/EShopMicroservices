namespace Ordering.API.Abstractions
{
    public interface IAggragate<T> : IAggragate, IEntity<T>
    {

    }

    public interface IAggragate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents {  get; }

        IDomainEvent[] ClearDomainEvents();
    }
}
