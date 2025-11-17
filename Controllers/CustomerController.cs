using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

/// <summary>
/// Controller for managing customers in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    /// <summary>
    /// The database context for accessing customer data.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for customer data access.</param>
    public CustomerController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a list of all customers.
    /// </summary>
    /// <returns>A list of customer entities.</returns>
    // GET: api/Customer
    [HttpGet]
    [HttpGet("Index")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    /// <summary>
    /// Retrieves a particular customer by their unique identifier.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <returns>The requested customer details, or <see cref="NotFoundResult"/> if not found.</returns>
    // api/Customer/5
    [HttpGet("Details/{id}")]
    public async Task<ActionResult<Customer>> GetParticularCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            return NotFound();
        return customer;
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customer">The customer entity to create.</param>
    /// <returns>The created customer entity.</returns>
    // POST: api/Customer/Create
    [HttpPost("Create")]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularCustomer), new { id = customer.CustomerId }, customer);
    }

    /// <summary>
    /// Updates an existing customer by their ID.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="customer">The updated customer data.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if successful,
    /// <see cref="BadRequestResult"/> if the ID does not match.
    /// </returns>
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
    {
        if (id != customer.CustomerId)
            return BadRequest();

        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Deletes a customer by their ID.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if deleted,
    /// <see cref="NotFoundResult"/> if no such customer exists.
    /// </returns>
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
