using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

/// <summary>
/// Controller for managing categories in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{

    /// <summary>
    /// The database context for accessing products, categories, and related entities.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryController"/>.
    /// </summary>
    /// <param name="context">Database context to access categories.</param>
    public CategoryController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all categories, including their related products.
    /// </summary>
    /// <returns>A list of categories with products.</returns>
    // GET: api/Category
    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        return await _context.Categories.Include(c => c.Products).ToListAsync();
    }

    /// <summary>
    /// Retrieves details for a particular category by ID, including its products.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <returns>The category details with products, or <see cref="NotFoundResult"/> if not found.</returns>
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

    
    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="category">The category to create.</param>
    /// <returns>The created category.</returns>
    // POST: api/Category/Create
    [HttpPost("Create")]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularCategory), new { id = category.CategoryId }, category);
    }


    /// <summary>
    /// Updates an existing category by ID.
    /// </summary>
    /// <param name="id">The ID of the category to update.</param>
    /// <param name="category">The updated category data.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if successful,
    /// <see cref="BadRequestResult"/> if the ID does not match.
    /// </returns>
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

    
    /// <summary>
    /// Deletes a category by ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if deleted,
    /// <see cref="NotFoundResult"/> if no such category exists.
    /// </returns>
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
