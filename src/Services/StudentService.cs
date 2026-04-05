using CSharpPlayground.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPlayground.Services
{
    /// <summary>
    /// Service layer for managing student data operations.
    /// Provides an abstraction over data access and business logic.
    /// </summary>
    public class StudentService
    {
        private readonly List<Student> _students = new();

        /// <summary>
        /// Adds a new student to the collection.
        /// </summary>
        /// <param name="student">Student object to add</param>
        /// <returns>The added student object</returns>
        /// <exception cref="ArgumentNullException">Thrown when student is null</exception>
        public Student AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");

            // Assign ID if not set
            if (student.Id == 0)
                student.Id = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;

            _students.Add(student);
            return student;
        }

        /// <summary>
        /// Retrieves all students from the collection.
        /// </summary>
        /// <returns>List of all students; empty list if none exist</returns>
        public List<Student> GetAllStudents()
        {
            return new List<Student>(_students);
        }

        /// <summary>
        /// Retrieves a specific student by ID.
        /// </summary>
        /// <param name="id">Student ID to search for</param>
        /// <returns>Student object if found; null otherwise</returns>
        public Student GetStudentById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Retrieves students matching a name pattern.
        /// </summary>
        /// <param name="name">Name or partial name to search for (case-insensitive)</param>
        /// <returns>List of matching students; empty list if none found</returns>
        /// <exception cref="ArgumentNullException">Thrown when name is null or empty</exception>
        public List<Student> GetStudentsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Name cannot be null or empty");

            return _students
                .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Gets the total count of students in the collection.
        /// </summary>
        /// <returns>Total number of students</returns>
        public int GetStudentCount()
        {
            return _students.Count;
        }
    }
}
