using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CSharpPlayground.Data;
using CSharpPlayground.Models;

// Build configuration from appsettings.json
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

Console.WriteLine("=== C# Playground: Entity Framework & Dapper Demo ===");

// Ensure db directory exists
var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "db");
if (!Directory.Exists(dbPath))
{
    Directory.CreateDirectory(dbPath);
}

// --- Entity Framework Demo ---
Console.WriteLine("\n--- Entity Framework Demo ---");

using var db = new SchoolContext(connectionString);
db.Database.EnsureCreated(); // Creates the .db file if it doesn't exist
db.Students.Add(new Student { Name = "Alice" });
db.SaveChanges();
Console.WriteLine("Inserted 'Alice' via Entity Framework");

var efStudents = db.Students.ToList();
Console.WriteLine("Students from EF query:");
foreach (var student in efStudents)
{
    Console.WriteLine($"  - Id: {student.Id}, Name: {student.Name}");
}

// --- Dapper Demo ---
DapperDemo.Run(connectionString);

Console.WriteLine("\n=== Demo Complete ===");

