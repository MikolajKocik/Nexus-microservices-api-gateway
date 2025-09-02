namespace Nexus.OrderService.Features.Orders;

public sealed class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Order> GetAll() => _repository.GetAll();

    public Order? GetById(int id) => _repository.GetById(id);

    public void Create(Order order) => _repository.Create(order);
}
