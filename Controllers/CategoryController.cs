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
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetParticularCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound();
        return category;
    }

    // POST: api/Category
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularCategory), new { id = category.CategoryId }, category);
    }

    // PUT: api/Category/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, Category category)
    {
        if (id != category.CategoryId)
            return BadRequest();

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Category/5
    [HttpDelete("{id}")]
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
