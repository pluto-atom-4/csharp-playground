using CSharpPlayground.Models;
using CSharpPlayground.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CSharpPlayground.Tests.Unit
{
    public class StudentServiceTests
    {
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            _service = new StudentService();
        }

        #region AddStudent Tests

        [Fact]
        public void AddStudent_WithValidStudent_ReturnsStudent()
        {
            // Arrange
            var student = new Student { Name = "John Doe", Email = "john@example.com", Grade = 10 };

            // Act
            var result = _service.AddStudent(student);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public void AddStudent_WithValidStudent_AssignsId()
        {
            // Arrange
            var student = new Student { Name = "Jane Doe", Email = "jane@example.com", Grade = 11 };

            // Act
            var result = _service.AddStudent(student);

            // Assert
            Assert.True(result.Id > 0);
        }

        [Fact]
        public void AddStudent_WithNullStudent_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.AddStudent(null));
        }

        [Fact]
        public void AddStudent_MultipleStudents_IncrementIds()
        {
            // Arrange
            var student1 = new Student { Name = "Student 1", Email = "s1@example.com", Grade = 9 };
            var student2 = new Student { Name = "Student 2", Email = "s2@example.com", Grade = 10 };

            // Act
            _service.AddStudent(student1);
            _service.AddStudent(student2);

            // Assert
            Assert.True(student1.Id < student2.Id);
        }

        #endregion

        #region GetAllStudents Tests

        [Fact]
        public void GetAllStudents_WithNoStudents_ReturnsEmptyList()
        {
            // Act
            var result = _service.GetAllStudents();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllStudents_WithStudents_ReturnsAllStudents()
        {
            // Arrange
            _service.AddStudent(new Student { Name = "Student A", Email = "a@example.com", Grade = 9 });
            _service.AddStudent(new Student { Name = "Student B", Email = "b@example.com", Grade = 10 });
            _service.AddStudent(new Student { Name = "Student C", Email = "c@example.com", Grade = 11 });

            // Act
            var result = _service.GetAllStudents();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetAllStudents_ReturnsNewListInstance()
        {
            // Arrange
            _service.AddStudent(new Student { Name = "Test", Email = "test@example.com", Grade = 10 });

            // Act
            var list1 = _service.GetAllStudents();
            var list2 = _service.GetAllStudents();

            // Assert
            Assert.NotSame(list1, list2);
        }

        #endregion

        #region GetStudentById Tests

        [Fact]
        public void GetStudentById_WithValidId_ReturnsStudent()
        {
            // Arrange
            var added = _service.AddStudent(new Student { Name = "Target", Email = "target@example.com", Grade = 10 });

            // Act
            var result = _service.GetStudentById(added.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Target", result.Name);
        }

        [Fact]
        public void GetStudentById_WithInvalidId_ReturnsNull()
        {
            // Act
            var result = _service.GetStudentById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetStudentById_WithMultipleStudents_ReturnsCorrectOne()
        {
            // Arrange
            var student1 = _service.AddStudent(new Student { Name = "First", Email = "first@example.com", Grade = 9 });
            var student2 = _service.AddStudent(new Student { Name = "Second", Email = "second@example.com", Grade = 10 });
            var student3 = _service.AddStudent(new Student { Name = "Third", Email = "third@example.com", Grade = 11 });

            // Act
            var result = _service.GetStudentById(student2.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Second", result.Name);
        }

        #endregion

        #region GetStudentsByName Tests

        [Fact]
        public void GetStudentsByName_WithExactMatch_ReturnsStudent()
        {
            // Arrange
            _service.AddStudent(new Student { Name = "Alice Johnson", Email = "alice@example.com", Grade = 10 });

            // Act
            var result = _service.GetStudentsByName("Alice Johnson");

            // Assert
            Assert.Single(result);
            Assert.Equal("Alice Johnson", result[0].Name);
        }

        [Fact]
        public void GetStudentsByName_WithPartialMatch_ReturnsStudents()
        {
            // Arrange
            _service.AddStudent(new Student { Name = "John Smith", Email = "john@example.com", Grade = 9 });
            _service.AddStudent(new Student { Name = "John Doe", Email = "jdoe@example.com", Grade = 10 });
            _service.AddStudent(new Student { Name = "Jane Smith", Email = "jane@example.com", Grade = 11 });

            // Act
            var result = _service.GetStudentsByName("John");

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetStudentsByName_CaseInsensitive_ReturnsStudents()
        {
            // Arrange
            _service.AddStudent(new Student { Name = "UPPERCASE", Email = "upper@example.com", Grade = 10 });

            // Act
            var result = _service.GetStudentsByName("uppercase");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetStudentsByName_WithNoMatches_ReturnsEmptyList()
        {
            // Arrange
            _service.AddStudent(new Student { Name = "Existing", Email = "existing@example.com", Grade = 10 });

            // Act
            var result = _service.GetStudentsByName("NotFound");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetStudentsByName_WithNullName_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.GetStudentsByName(null));
        }

        [Fact]
        public void GetStudentsByName_WithEmptyString_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.GetStudentsByName(""));
        }

        #endregion

        #region GetStudentCount Tests

        [Fact]
        public void GetStudentCount_WithNoStudents_ReturnsZero()
        {
            // Act
            var result = _service.GetStudentCount();

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetStudentCount_WithStudents_ReturnsCorrectCount()
        {
            // Arrange
            _service.AddStudent(new Student { Name = "S1", Email = "s1@example.com", Grade = 9 });
            _service.AddStudent(new Student { Name = "S2", Email = "s2@example.com", Grade = 10 });
            _service.AddStudent(new Student { Name = "S3", Email = "s3@example.com", Grade = 11 });

            // Act
            var result = _service.GetStudentCount();

            // Assert
            Assert.Equal(3, result);
        }

        #endregion
    }
}
