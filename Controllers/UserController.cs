using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;


/// <summary>
/// Controller for managing users in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    /// <summary>
    /// The database context for accessing users and related entities.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for user data access.</param>
    public UserController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all users in the system.
    /// </summary>
    /// <returns>A list of all users.</returns>
    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    /// <summary>
    /// Retrieves a particular user by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>
    /// The requested user details,
    /// or <see cref="NotFoundResult"/> if not found.
    /// </returns>
    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetParticularUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        return user;
    }

    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <returns>The created user with its unique identifier.</returns>
    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetParticularUser), new { id = user.UserId }, user);
    }


    /// <summary>
    /// Updates an existing user by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user entity.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if updated,
    /// <see cref="BadRequestResult"/> if the ID does not match.
    /// </returns>
    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.UserId)
            return BadRequest();

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if deleted,
    /// <see cref="NotFoundResult"/> if no such user exists.
    /// </returns>
    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}