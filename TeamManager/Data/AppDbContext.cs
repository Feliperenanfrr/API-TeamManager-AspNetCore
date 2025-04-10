using Microsoft.EntityFrameworkCore;
using TeamManager.Model;

namespace TeamManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Athlete> Athletes { get; set; }

}