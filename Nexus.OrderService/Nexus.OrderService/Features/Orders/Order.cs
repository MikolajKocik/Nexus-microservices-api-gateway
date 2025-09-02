namespace Nexus.OrderService.Features.Orders;

public sealed class Order
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CustomerName { get; private set; } = default!;

    private List<int> _productsIds = new();
    public IReadOnlyList<int> ProductIds => _productsIds.AsReadOnly();

    public Order(DateTime createAt, string customerName, List<int> productsIds)
    {
        CreatedAt = createAt;
        CustomerName = customerName;
        _productsIds = productsIds;
    }

    public void Create(Order order)
    {
        order.Id = _productsIds.Count + 1;
        order.CreatedAt = DateTime.UtcNow;
        _productsIds.Add(order.Id);
    }

    public static OrderDto ToDto(Order order) => new()
    {
        CreatedAt = order.CreatedAt,
        CustomerName = order.CustomerName,
        ProductIds = order.ProductIds.ToList()
    };

    public static Order FromDto(OrderDto dto) => new(
        dto.CreatedAt,
        dto.CustomerName,
        dto.ProductIds.ToList()
    );
}
