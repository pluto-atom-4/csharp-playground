# CSharpPlayground

## Overview

This is a repository to assess **LTS** (Long-Term Support) and **latest .NET programming** concepts and best practices.

## Purpose

The CSharpPlayground project is designed to explore and evaluate:
- Modern C# language features
- Entity Framework Core for data access
- Dapper for micro-ORM operations
- Best practices in .NET development
- Comparison between ORM patterns

## Project Structure

```
csharp-playground/
├── src/
│   ├── Models/
│   │   └── Student.cs                    # Student entity model
│   ├── Services/
│   │   └── StudentService.cs             # Service layer for data operations
│   └── CSharpPlayground/
│       ├── MyEF.cs                       # Entity Framework demonstration
│       ├── MyDapper.cs                   # Dapper ORM demonstration
│       └── Program.cs                    # Main entry point
├── tests/
│   ├── Unit/
│   │   └── StudentServiceTests.cs        # 19 unit tests for StudentService
│   └── CSharpPlayground.Tests/
│       └── [existing tests]
├── Data/                                  # Entity Framework context & models
├── appsettings.json                      # Configuration
├── csharp-playground.csproj              # Main project file
├── csharp-playground.sln                 # Solution file
├── REORGANIZATION_SUMMARY.md             # Detailed reorganization guide
└── IMPLEMENTATION_COMPLETE.md            # Implementation status report
```

### Directory Breakdown

- **src/Models/** - Domain models (Student entity)
- **src/Services/** - Business logic abstraction layer (StudentService)
- **src/CSharpPlayground/** - Main application logic and demonstrations
- **tests/Unit/** - Unit tests (StudentServiceTests with 19 tests)
- **Data/** - Entity Framework context and database models
- **db/** - SQLite database file

## Architecture

### Service Layer Pattern
This project implements a service layer pattern for clean separation of concerns:

- **Models** - Domain entities (Student)
- **Services** - Business logic abstraction (StudentService)
- **Tests** - Comprehensive unit tests for services

The StudentService provides an abstraction over data operations, making it easy to:
- Test business logic independently
- Swap implementations (in-memory → database)
- Maintain and extend functionality
- Support dependency injection

### Key Features

- **StudentService** (5 methods):
  - AddStudent() - Create new student
  - GetAllStudents() - Retrieve all students
  - GetStudentById() - Find by ID
  - GetStudentsByName() - Search by name (partial match, case-insensitive)
  - GetStudentCount() - Get total count

- **Comprehensive Testing** (19 tests):
  - Unit tests for each method
  - Edge case coverage
  - Null/empty input validation
  - Error condition handling

## Running the Project

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "StudentServiceTests"

# Run the main application
dotnet run
```

## Testing

The project includes comprehensive unit tests:
- **StudentServiceTests.cs** - 19 xUnit tests covering:
  - AddStudent (4 tests)
  - GetAllStudents (3 tests)
  - GetStudentById (3 tests)
  - GetStudentsByName (6 tests)
  - GetStudentCount (2 tests)
  - Integration scenarios

Run tests with:
```bash
dotnet test
```

Expected output: All 19 tests pass ✅

## Database

The project uses SQLite with database file located at `db/csharp-playground.db`.

---

**Assessment Focus**: Evaluating LTS and latest .NET programming patterns and methodologies.

