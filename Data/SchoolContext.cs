using Microsoft.EntityFrameworkCore;
using CSharpPlayground.Models;
using Microsoft.Extensions.Configuration;

namespace CSharpPlayground.Data;

public class SchoolContext : DbContext
{
    private readonly string _connectionString;

    public SchoolContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}

