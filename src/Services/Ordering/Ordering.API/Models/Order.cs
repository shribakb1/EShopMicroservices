using Ordering.API.Abstractions;

namespace Ordering.API.Models
{
    public class Order : Aggregate<Guid>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public Guid CustomerId { get; set; } = default;

        public string OrderName { get; set; }
    }

}
