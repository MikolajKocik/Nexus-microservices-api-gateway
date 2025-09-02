namespace Nexus.OrderService.Features.Orders;

public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    Order? GetById(int id);
    void Create(Order order);  
}
