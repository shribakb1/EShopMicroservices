﻿using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; set; } = default;
        public OrderName OrderName { get; set; } = default;
        public Address ShippingAddress { get; private set; } = default;
        public Address BillingAddress { get; private set; } = default;
        public Payment Payment { get; private set; } = default;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice 
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            Order order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            OrderItem orderItem = new OrderItem(Id, productId, quantity, price);
            
            _orderItems.Add(orderItem);
        }

        public void Remove(OrderItemId orderItemId)
        {
            OrderItem? orderItem = _orderItems.FirstOrDefault(x => x.Id == orderItemId);
            if (orderItem is null)
            {
                throw new InvalidOperationException($"Order item with id {orderItemId} not found in order {Id}");
            }
            _orderItems.Remove(orderItem);     
        }
    }

}
