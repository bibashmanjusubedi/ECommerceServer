using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;
using ecomServer.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ecomServerDbContext _context;

    public ProductController(ecomServerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    [HttpGet("Details/{id}")]
    public async Task<ActionResult<Product>> GetParticularProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound();
        return product;
    }

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
