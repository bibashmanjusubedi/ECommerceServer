using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;


/// <summary>
/// Controller for managing roles in the e-commerce system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    /// <summary>
    /// The database context for accessing roles and related entities.
    /// </summary>
    private readonly ecomServerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for role data access.</param>
    public RoleController(ecomServerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all roles in the system.
    /// </summary>
    /// <returns>A list of all roles.</returns>
    // GET: api/Role
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    /// <summary>
    /// Retrieves a particular role by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the role to retrieve.</param>
    /// <returns>
    /// The requested role details,
    /// or <see cref="NotFoundResult"/> if not found.
    /// </returns>s
    // GET: api/Role/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetParticularRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
            return NotFound();

        return role;
    }


    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="role">The role entity to create.</param>
    /// <returns>The created role with its unique identifier.</returns>
    // POST: api/Role
    [HttpPost]
    public async Task<ActionResult<Role>> CreateRole(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetParticularRole), new { id = role.RoleId }, role);
    }

    /// <summary>
    /// Updates an existing role by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the role to update.</param>
    /// <param name="role">The updated role entity.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if updated,
    /// <see cref="BadRequestResult"/> if the ID does not match.
    // PUT: api/Role/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, Role role)
    {
        if (id != role.RoleId)
            return BadRequest();

        _context.Entry(role).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deletes a role by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the role to delete.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> if deleted,
    /// <see cref="NotFoundResult"/> if no such role exists.
    /// </returns>
    // DELETE: api/Role/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
            return NotFound();

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
