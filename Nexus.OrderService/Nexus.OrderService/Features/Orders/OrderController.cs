using Microsoft.AspNetCore.Mvc;

namespace Nexus.OrderService.Features.Orders;

[ApiController]
[Route("api/v{version:apiVersion}/orders")]
[ApiVersion("1.0")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrderDto>> GetAll()
    {
        IEnumerable<Order> orders = _orderService.GetAll();

        IEnumerable<OrderDto> dto = orders.Select(Order.ToDto);

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public ActionResult<OrderDto> GetById(int id)
    {
        Order? order = _orderService.GetById(id);
        if (order is null) return NotFound();

        OrderDto dto = Order.ToDto(order);

        return Ok(dto);
    }

    [HttpPost]
    public ActionResult<Order> Create([FromBody] OrderDto dto)
    {
        Order order = Order.FromDto(dto);

        _orderService.Create(order);
        return CreatedAtAction(nameof(GetById), new { id = order.Id, version = "1" }, order);
    }
}
