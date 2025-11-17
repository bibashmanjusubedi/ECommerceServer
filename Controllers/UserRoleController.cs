using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

/// <summary>
/// Controller for managing user-role relationships in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    /// <summary>
    /// The database context for accessing user roles and related entities.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for user role data access.</param>
    public UserRoleController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all user-role associations.
    /// </summary>
    /// <returns>A list of all user-role relationships.</returns>
    // GET: api/UserRole
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRoles()
    {
        return await _context.UserRoles.ToListAsync();
    }


    /// <summary>
    /// Retrieves a particular user-role association by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user-role relationship to retrieve.</param>
    /// <returns>
    /// The requested user-role association,
    /// or <see cref="NotFoundResult"/> if not found.
    // GET: api/UserRole/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserRole>> GetParticularUserRole(int id)
    {
        var userRole = await _context.UserRoles.FindAsync(id);
        if (userRole == null)
            return NotFound();
        return userRole;
    }


    /// <summary>
    /// Creates a new user-role association.
    /// </summary>
    /// <param name="userRole">The user-role entity to create.</param>
    /// <returns>The created user-role association with its ID.</returns>
    // POST: api/UserRole
    [HttpPost]
    public async Task<ActionResult<UserRole>> CreateUserRole(UserRole userRole)
    {
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularUserRole), new { id = userRole.UserRoleId }, userRole);
    }

    /// <summary>
    /// Updates an existing user-role association by ID.
    /// </summary>
    /// <param name="id">The ID of the user-role association to update.</param>
    /// <param name="userRole">The updated user-role entity.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if updated,
    /// <see cref="BadRequestResult"/> if the ID does not match.
    // PUT: api/UserRole/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserRole(int id, UserRole userRole)
    {
        if (id != userRole.UserRoleId)
            return BadRequest();

        _context.Entry(userRole).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Deletes a user-role association by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user-role association to delete.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if deleted,
    /// <see cref="NotFoundResult"/> if no such association exists.
    /// </returns>
    // DELETE: api/UserRole/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserRole(int id)
    {
        var userRole = await _context.UserRoles.FindAsync(id);
        if (userRole == null)
            return NotFound();

        _context.UserRoles.Remove(userRole);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
