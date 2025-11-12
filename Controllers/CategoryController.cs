using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ecomServerDbContext _context;

    public CategoryController(ecomServerDbContext context)
    {
        _context = context;
    }

    // GET: api/Category
    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        return await _context.Categories.Include(c => c.Products).ToListAsync();
    }

    // GET: api/Category/Details/5
    [HttpGet("Details/{id}")]
    public async Task<ActionResult<Category>> GetParticularCategory(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
            return NotFound();
        return category;
    }


    // POST: api/Category/Create
    [HttpPost("Create")]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularCategory), new { id = category.CategoryId }, category);
    }

    // PUT: api/Category/Update/5
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateCategory(int id, Category category)
    {
        if (id != category.CategoryId)
            return BadRequest();

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Category/Delete/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
