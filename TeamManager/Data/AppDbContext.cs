﻿using Microsoft.EntityFrameworkCore;
using TeamManager.Model;

namespace TeamManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Train> Trains { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
}
