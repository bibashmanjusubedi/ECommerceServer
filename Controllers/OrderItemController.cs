using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly ecomServerDbContext _context;

    public OrderItemController(ecomServerDbContext context)
    {
        _context = context;
    }

    // GET: api/OrderItem
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
    {
        return await _context.OrderItems.ToListAsync();
    }

    // GET: api/OrderItem/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetParticularOrderItem(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem == null)
            return NotFound();
        return orderItem;
    }

    // POST: api/OrderItem
    [HttpPost]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularOrderItem), new { id = orderItem.OrderItemId }, orderItem);
    }

    // PUT: api/OrderItem/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
    {
        if (id != orderItem.OrderItemId)
            return BadRequest();

        _context.Entry(orderItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/OrderItem/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem == null)
            return NotFound();

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
