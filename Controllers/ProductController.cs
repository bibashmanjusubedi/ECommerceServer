using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;
using ecomServer.DTOs;

/// <summary>
/// Controller for managing products in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    /// <summary>
    /// The database context for accessing products, categories, and related entities.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for data access.</param
    public ProductController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a list of all products.
    /// </summary>
    /// <returns>A list of products.</returns>
    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return await _context.Products
            .Include(p => p.Category)  // Load related Category data
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The product ID.</param>
    /// <returns>The requested product details.</returns>
    [HttpGet("Details/{id}")]
    public async Task<ActionResult<Product>> GetParticularProduct(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)  // Eagerly load the Category
            .FirstOrDefaultAsync(p => p.ProductId == id);
        if (product == null)
            return NotFound();
        return product;
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="productDto">The product information.</param>
    /// <returns>The created product.</returns>
    [HttpPost("Create")]
    public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductCreateDto dto)
    {
        byte[] imageData = null;
        string imageMimeType = null;

        if (dto.ImageFile != null)
        {
            using (var ms = new MemoryStream())
            {
                await dto.ImageFile.CopyToAsync(ms);
                imageData = ms.ToArray();
            }
            imageMimeType = dto.ImageFile.ContentType;
        }

        var product = new Product
        {
            Name = dto.Name,
            SKU = dto.SKU,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            ImageData = imageData,
            ImageMimeType = imageMimeType
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularProduct), new { id = product.ProductId }, product);
    }

    /// <summary>
    /// Updates an existing product by its ID.
    /// </summary>
    /// <returns>
    /// Returns <see cref="NoContentResult"/> if the update is successful,
    /// </returns>
    /// <remarks>
    /// This endpoint updates the product’s name, SKU, price, category, and optionally the image.
    /// The product is identified by the given <paramref name="id"/>.
    /// </remarks>
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        product.Name = dto.Name;
        product.SKU = dto.SKU;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        if (dto.ImageFile != null)
        {
            using (var ms = new MemoryStream())
            {
                await dto.ImageFile.CopyToAsync(ms);
                product.ImageData = ms.ToArray();
            }
            product.ImageMimeType = dto.ImageFile.ContentType;
        }

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Deletes a product by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>
    /// Returns <see cref="NoContentResult"/> if the product is successfully deleted,
    /// </returns>
    /// <remarks>
    /// This endpoint permanently removes the product with the specified <paramref name="id"/> from the database.
    /// </remarks>
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
