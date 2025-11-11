using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomServer.DAL;
using ecomServer.Models;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly ecomServerDbContext _context;

    public UserRoleController(ecomServerDbContext context)
    {
        _context = context;
    }

    // GET: api/UserRole
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRoles()
    {
        return await _context.UserRoles.ToListAsync();
    }

    // GET: api/UserRole/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserRole>> GetParticularUserRole(int id)
    {
        var userRole = await _context.UserRoles.FindAsync(id);
        if (userRole == null)
            return NotFound();
        return userRole;
    }

    // POST: api/UserRole
    [HttpPost]
    public async Task<ActionResult<UserRole>> CreateUserRole(UserRole userRole)
    {
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetParticularUserRole), new { id = userRole.UserRoleId }, userRole);
    }

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
