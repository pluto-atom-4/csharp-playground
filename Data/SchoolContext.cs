using CSharpPlayground.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpPlayground.Data;

public class SchoolContext(string connectionString) : DbContext
{
    public DbSet<Student> Students { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}

