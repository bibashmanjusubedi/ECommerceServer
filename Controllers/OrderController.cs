using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;
using ecomServer.DTOs;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ecomServerDbContext _context;

    public OrderController(ecomServerDbContext context)
    {
        _context = context;
    }

    // GET: api/Order
    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
    }

    // GET: api/Order/Details/5
    [HttpGet("Details/{id}")]
    public async Task<ActionResult<Order>> GetParticularOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id);
        if (order == null)
            return NotFound();
        return order;
    }

    // POST: api/Order/Create
    [HttpPost("Create")]
    public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto dto)
    {
        var order = new Order
        {
            OrderDate = DateTime.SpecifyKind(dto.OrderDate, DateTimeKind.Utc),
            CustomerId = dto.CustomerId,
            OrderItems = dto.OrderItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Reload the order with navigation properties included
        var orderWithDetails = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

        return CreatedAtAction(nameof(GetParticularOrder), new { id = order.OrderId }, orderWithDetails);
    }



    // PUT: api/Order/Update/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto dto)
    {
        var existingOrder = await _context.Orders
        .Include(o => o.OrderItems)
        .FirstOrDefaultAsync(o => o.OrderId == id);

        if (existingOrder == null)
            return NotFound();

        // Update order main properties
        existingOrder.OrderDate = DateTime.SpecifyKind(dto.OrderDate, DateTimeKind.Utc);
        existingOrder.CustomerId = dto.CustomerId;

        // Remove all existing order items
        _context.OrderItems.RemoveRange(existingOrder.OrderItems);

        // Add updated order items from DTO
        existingOrder.OrderItems = dto.OrderItems.Select(item => new OrderItem
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice
        }).ToList();

        await _context.SaveChangesAsync();

        return NoContent();

    }

    // DELETE: api/Order/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
            return NotFound();

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
