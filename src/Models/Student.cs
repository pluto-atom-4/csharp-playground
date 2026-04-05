using System;

namespace CSharpPlayground.Models
{
    /// <summary>
    /// Represents a student entity in the system.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the student.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the grade or class level of the student.
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Gets or sets the date the student enrolled.
        /// </summary>
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the Student class.
        /// </summary>
        public Student()
        {
            EnrollmentDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the Student class with specified properties.
        /// </summary>
        public Student(string name, string email, int grade)
        {
            Name = name;
            Email = email;
            Grade = grade;
            EnrollmentDate = DateTime.UtcNow;
        }
    }
}
