using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomServer.DTOs;

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
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
    {
        var orderItems = await _context.OrderItems.
            Include(oi => oi.Order).
            Include(oi => oi.Product).
            ToListAsync();

        return orderItems;
    }

    // GET: api/OrderItem/5
    [HttpGet("Details/{id}")]
    public async Task<ActionResult<OrderItem>> GetParticularOrderItem(int id)
    {
        var orderItem = await _context.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Product)
            .FirstOrDefaultAsync(oi => oi.OrderItemId == id);
        if (orderItem == null)
            return NotFound();
        return orderItem;
    }

    // POST: api/OrderItem/Create
    [HttpPost("Create")]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItemCreateDto dto)
    {
        var orderItem = new OrderItem
        {
            OrderId = dto.OrderId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice
        };

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        var createdOrderItem = await _context.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Product)
            .FirstOrDefaultAsync(oi => oi.OrderItemId == orderItem.OrderItemId);

        return CreatedAtAction(nameof(GetParticularOrderItem), new { id = orderItem.OrderItemId }, createdOrderItem);
    }

    // PUT: api/OrderItem/Update/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, OrderItemUpdateDto dto)
    {
        if (id != dto.OrderItemId)
            return BadRequest();

        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem == null)
            return NotFound();

        // Update scalar properties from DTO
        orderItem.OrderId = dto.OrderId;
        orderItem.ProductId = dto.ProductId;
        orderItem.Quantity = dto.Quantity;
        orderItem.UnitPrice = dto.UnitPrice;

        // Save changes
        await _context.SaveChangesAsync();

        // Load related entities for internal use (optional)
        var updatedOrderItem = await _context.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Product)
            .FirstOrDefaultAsync(oi => oi.OrderItemId == id);

        if (updatedOrderItem == null)
            return NotFound();

        // Return NoContent() to client
        return NoContent();
    }


    // DELETE: api/OrderItem/5
    [HttpDelete("Delete/{id}")]
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
