using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Model;
using Transaction = System.Transactions.Transaction;

namespace TeamManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Train> Trains { get; set; }
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    public DbSet<User> Users { get; set; }
}
