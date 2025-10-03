﻿
namespace Ordering.Domain.Abstractions
{
    public class Aggregate<TId> : Entity<TId>, IAggragate<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEven)
        {
            _domainEvents.Add(domainEven);
        }
        
        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();

            _domainEvents.Clear();

            return dequeuedEvents;
        }
    }
}
