using System.ComponentModel.DataAnnotations;

namespace CSharpPlayground.Models;

public class Student
{
    public int Id { get; init; }
    [MaxLength(100)]
    public required string Name { get; init; }
}
