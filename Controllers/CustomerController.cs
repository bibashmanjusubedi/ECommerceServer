using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ecomServerDbContext _context;

    public CustomerController(ecomServerDbContext context)
    {
        _context = context;
    }

    // GET: api/Customer
    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        return await _context.Customers.ToListAsync();
    }


    // api/Customer/5
    [HttpGet("Details/{id}")]
    public async Task<ActionResult<Customer>> GetParticularCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            return NotFound();
        return customer;
    }

    // POST: api/Customer/Create
    [HttpPost("Create")]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularCustomer), new {id = customer.CustomerId}, customer);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateCustomer(int id,Customer customer)
    {
        if (id != customer.CustomerId)
            return BadRequest();
        
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            return NotFound();

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
