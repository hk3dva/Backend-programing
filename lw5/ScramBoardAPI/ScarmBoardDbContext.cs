// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using ScramBoardAPI.Models;

namespace ScramBoardAPI;

public class ScarmBoardDbContext : DbContext
{
    public DbSet<Board> Boards { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<CTask> Tasks { get; set; }

    public ScarmBoardDbContext() : base()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Lw5;Trusted_Connection=True;"); //swape name db
        //optionsBuilder.UseNpgsql(@"host=localhost;port=7041;database=Lw5;user id=postgres;password=password;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Board>()
            .HasMany(b => b.Columns);
        modelBuilder.Entity<Column>().HasMany(c => c.Tasks);
    }


    public ScarmBoardDbContext(DbContextOptions<ScarmBoardDbContext> options)
        :base(options)
    {
        Database.EnsureCreated();
    }


}
