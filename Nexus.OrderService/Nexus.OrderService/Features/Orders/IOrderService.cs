namespace Nexus.OrderService.Features.Orders;

public interface IOrderService
{
    IEnumerable<Order> GetAll();
    Order? GetById(int id);
    void Create(Order order);
}
