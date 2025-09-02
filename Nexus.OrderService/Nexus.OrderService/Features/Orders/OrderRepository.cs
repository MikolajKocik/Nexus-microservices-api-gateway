namespace Nexus.OrderService.Features.Orders;

public class OrderRepository : IOrderRepository
{
    private static readonly List<Order> _orders = new();

    public IEnumerable<Order> GetAll() => _orders;

    public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public void Create(Order order)
    {
        order = new Order(DateTime.UtcNow, order.CustomerName, order.ProductIds.ToList());

        order.Create(order);
    }
}
