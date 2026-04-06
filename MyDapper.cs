using CSharpPlayground.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CSharpPlayground;

public static class DapperDemo
{
    public static void Run(string connectionString)
    {
        Console.WriteLine("\n--- Dapper Demo ---");
        using var connection = new SqliteConnection(connectionString);
        
        // 1. Insert data using raw SQL
        var sql = "INSERT INTO Students (Name) VALUES (@Name)";
        connection.Execute(sql, new { Name = "Bob" });
        Console.WriteLine("Inserted 'Bob' via Dapper");
        
        // 2. Query data back into a list of objects
        var students = connection.Query<Student>("SELECT * FROM Students").ToList();
        Console.WriteLine("Students from Dapper query:");
        foreach (var student in students)
        {
            Console.WriteLine($"  - Id: {student.Id}, Name: {student.Name}");
        }
    }
}