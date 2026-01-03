using BookStore.Domain.Abstractions;
using System.Linq.Expressions;

namespace BookStore.Contracts.Infrastructure.Database.Repositories.Models;

public class OrderingParameters<TEntity> where TEntity : IEntity
{
    public Expression<Func<TEntity, object>> OrderingProperty { get; set; } = default!;
    public Order Order { get; set; } = Order.Ascending;

    private OrderingParameters()
    { }

    public static OrderingParameters<TEntity> Ascending(Expression<Func<TEntity, object>> orderingProperty)
    {
        return new OrderingParameters<TEntity>
        {
            OrderingProperty = orderingProperty,
            Order = Order.Ascending,
        };
    }

    public static OrderingParameters<TEntity> Descending(Expression<Func<TEntity, object>> orderingProperty)
    {
        return new OrderingParameters<TEntity>
        {
            OrderingProperty = orderingProperty,
            Order = Order.Descending,
        };
    }

    public static OrderingParameters<TEntity> Custom(Expression<Func<TEntity, object>> orderingProperty, Order order)
    {
        return new OrderingParameters<TEntity>
        {
            OrderingProperty = orderingProperty,
            Order = order,
        };
    }
}
