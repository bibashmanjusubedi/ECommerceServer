using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;
using ecomServer.DTOs;

/// <summary>
/// Controller for managing orders in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    /// <summary>
    /// The database context for accessing order data and related entities.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for order data access.</param>
    public OrderController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all orders, including their associated customers and order items.
    /// </summary>
    /// <returns>A list of orders with customer and order item details.</returns>
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

    /// <summary>
    /// Retrieves a particular order by its unique identifier, including customer and order items.
    /// </summary>
    /// <param name="id">The order ID.</param>
    /// <returns>
    /// The requested order details with navigation properties,
    /// or <see cref="NotFoundResult"/> if not found.
    /// </returns>
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

    /// <summary>
    /// Creates a new order with specified order items for a customer.
    /// </summary>
    /// <param name="dto">The order creation data transfer object.</param>
    /// <returns>The created order entity including details.</returns>
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

    /// <summary>
    /// Updates an existing order and its associated order items by order ID.
    /// </summary>
    /// <param name="id">The order ID to update.</param>
    /// <param name="dto">The updated order data transfer object.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if updated,
    /// <see cref="NotFoundResult"/> if no such order exists.
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

    /// <summary>
    /// Deletes an order by its unique identifier.
    /// </summary>
    /// <param name="id">The order ID to delete.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if deleted,
    /// <see cref="NotFoundResult"/> if not found.
    /// </returns>
    // DELETE: api/Order/Delete/5
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
