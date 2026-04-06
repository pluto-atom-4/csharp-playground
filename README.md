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
├── Data/                         # Entity Framework context
│   └── SchoolContext.cs          # EF Core DbContext for Student entity
├── db/                           # Local database files
│   └── csharp-playground.db      # SQLite database file
├── Models/
│   └── Student.cs                # Student entity model
├── tests/
│   └── Unit/
├── .gitignore                    # Git Ignore file
├── appsettings.json              # Configuration
├── csharp-playground.csproj      # Main project file
├── csharp-playground.sln         # Solution file
├── MyDapper.cs                   # Dapper ORM demonstration
├── MyEF.cs                       # Entity Framework demonstration
├── Program.cs                    # Main entry point
└── README.md                     # This file
```

### Directory Breakdown

- **tests/Unit/** - Unit tests (StudentServiceTests with 19 tests)
- **Data/** - Entity Framework context and database models
- **db/** - SQLite database file

## Running the Project

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run the main application
dotnet run
```


## Database

The project uses SQLite with database file located at `db/csharp-playground.db`.

---

**Assessment Focus**: Evaluating LTS and latest .NET programming patterns and methodologies.

