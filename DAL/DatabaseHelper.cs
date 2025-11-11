using Microsoft.EntityFrameworkCore;

namespace ecomServer.DAL;

public class ecomServerDbContext : DbContext
{
    public ecomServerDbContext(DbContextOptions<ecomServerDbContext> options): base(options)
    {

    }
}