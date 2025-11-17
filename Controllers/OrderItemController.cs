using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomServer.DTOs;

/// <summary>
/// Controller for managing order items in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    /// <summary>
    /// The database context for accessing order item data and related entities.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderItemController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for order item data access.</param>
    public OrderItemController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all order items, including their associated orders and products.
    /// </summary>
    /// <returns>A list of order items with order and product details.</returns>
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


    /// <summary>
    /// Retrieves a particular order item by its unique identifier, including order and product details.
    /// </summary>
    /// <param name="id">The order item ID.</param>
    /// <returns>
    /// The requested order item with navigation properties,
    /// or <see cref="NotFoundResult"/> if not found.
    /// </returns>
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

    /// <summary>
    /// Creates a new order item.
    /// </summary>
    /// <param name="dto">The order item creation data transfer object.</param>
    /// <returns>The created order item entity with details.</returns>
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

    /// <summary>
    /// Updates an existing order item by its ID.
    /// </summary>
    /// <param name="id">The ID of the order item to update.</param>
    /// <param name="dto">The updated order item data transfer object.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if updated,
    /// <see cref="BadRequestResult"/> if the ID does not match,
    /// <see cref="NotFoundResult"/> if no such order item exists.
    /// </returns>
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

    /// <summary>
    /// Deletes an order item by its ID.
    /// </summary>
    /// <param name="id">The ID of the order item to delete.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if deleted,
    /// <see cref="NotFoundResult"/> if not found.
    /// </returns>
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
