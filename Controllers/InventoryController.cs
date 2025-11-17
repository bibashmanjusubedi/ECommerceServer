using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

/// <summary>
/// Controller for managing inventory entries in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    /// <summary>
    /// The database context for accessing inventory, products, and related entities.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="InventoryController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for data access.</param>
    public InventoryController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all inventory entries, including their related products.
    /// </summary>
    /// <returns>A list of inventories with products.</returns>
    // GET: api/Inventory
    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Inventory>>> GetAllInventories()
    {
        return await _context.Inventories.Include(i => i.Product).ToListAsync();
    }


    /// <summary>
    /// Retrieves a particular inventory entry by its unique ID, including its related product.
    /// </summary>
    /// <param name="id">The inventory ID.</param>
    /// <returns>
    /// The inventory details with product, or <see cref="NotFoundResult"/> if not found.
    /// </returns>
    // GET: api/Inventory/5
    [HttpGet("Details/{id}")]
    public async Task<ActionResult<Inventory>> GetParticularInventory(int id)
    {
        var inventory = await _context.Inventories
        .Include(i => i.Product)  // Eagerly load related Product
        .FirstOrDefaultAsync(i => i.InventoryId == id);
        if (inventory == null)
            return NotFound();
        return inventory;
    }


    /// <summary>
    /// Creates a new inventory entry.
    /// </summary>
    /// <param name="inventory">The inventory item to create.</param>
    /// <returns>The created inventory item.</returns>
    // POST: api/Inventory/Create
    [HttpPost("Create")]
    public async Task<ActionResult<Inventory>> CreateInventory(Inventory inventory)
    {
        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();

        // Load the related Product explicitly
        await _context.Entry(inventory).Reference(i => i.Product).LoadAsync();

        return CreatedAtAction(nameof(GetParticularInventory), new { id = inventory.InventoryId }, inventory);
    }

    /// <summary>
    /// Updates an existing inventory entry by its ID.
    /// </summary>
    /// <param name="id">The ID of the inventory to update.</param>
    /// <param name="inventory">The updated inventory data.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if successful,
    /// <see cref="BadRequestResult"/> if the ID does not match.
    /// </returns>
    // PUT: api/Inventory/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateInventory(int id, Inventory inventory)
    {
        if (id != inventory.InventoryId)
            return BadRequest();

        _context.Entry(inventory).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }


    /// <summary>
    /// Deletes an inventory entry by its ID.
    /// </summary>
    /// <param name="id">The ID of the inventory to delete.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if deleted,
    /// <see cref="NotFoundResult"/> if not found.
    /// </returns>
    // DELETE: api/Inventory/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteInventory(int id)
    {
        var inventory = await _context.Inventories.FindAsync(id);
        if (inventory == null)
            return NotFound();

        _context.Inventories.Remove(inventory);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
