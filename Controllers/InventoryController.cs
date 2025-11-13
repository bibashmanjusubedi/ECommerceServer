using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly ecomServerDbContext _context;

    public InventoryController(ecomServerDbContext context)
    {
        _context = context;
    }

    // GET: api/Inventory
    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Inventory>>> GetAllInventories()
    {
        return await _context.Inventories.Include(i => i.Product).ToListAsync();
    }

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
