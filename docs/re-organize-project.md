# Re-organize and Enhance the C# Playground Project to Follow Best Practices in .NET Development

## Overview

This document outlines a comprehensive reorganization of the CSharpPlayground project to align with [Microsoft's official .NET project structure guidance](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props) and industry best practices. The reorganization improves code maintainability, testability, and follows the separation of concerns principle.

## Current State Assessment

### Issues with Current Structure
- **Mixed responsibility**: Program logic spread across MyEF.cs and MyDapper.cs
- **Lack of clear entry point**: Program.cs not being used as main orchestrator
- **Missing test infrastructure**: No dedicated unit test project structure
- **Unclear feature boundaries**: Data access patterns not properly organized

### What Works Well
- ✅ Models and Data layers exist (Student.cs, SchoolContext.cs)
- ✅ Dual implementation pattern (EF Core vs Dapper) for comparison
- ✅ Configuration-driven approach with appsettings.json

---

## Target Project Structure

Following Microsoft's official .NET SDK guidance and industry best practices:

```
csharp-playground/
├── src/
│   └── CSharpPlayground/                              # Main application project
│       ├── Models/
│       │   └── Student.cs                             # Entity definitions
│       ├── Data/
│       │   ├── SchoolContext.cs                       # EF Core DbContext
│       │   └── DatabaseInitializer.cs                 # Database seeding & initialization
│       ├── Features/
│       │   └── DataAccess/
│       │       ├── IStudentRepository.cs              # Interface abstraction
│       │       ├── StudentEFRepository.cs             # EF Core implementation
│       │       └── StudentDapperRepository.cs         # Dapper implementation
│       ├── Program.cs                                 # Console app entry point (refactored)
│       ├── Constants.cs                               # Application constants
│       ├── appsettings.json                           # Configuration (connection strings)
│       ├── CSharpPlayground.csproj                    # Project file (.NET 8)
│       └── .gitignore                                 # Local excludes
├── tests/
│   └── CSharpPlayground.Tests/
│       ├── Unit/
│       │   ├── DataAccess/
│       │   │   ├── StudentEFRepositoryTests.cs       # EF Core unit tests
│       │   │   └── StudentDapperRepositoryTests.cs   # Dapper unit tests
│       │   └── Models/
│       │       └── StudentTests.cs                    # Entity model tests
│       ├── Integration/
│       │   ├── DataAccess/
│       │   │   └── RepositoryIntegrationTests.cs     # Real DB tests
│       │   └── Fixtures/
│       │       └── DatabaseFixture.cs                # Shared test database setup
│       ├── xunit.runner.json                         # xUnit configuration
│       ├── CSharpPlayground.Tests.csproj             # Test project file
│       └── .gitignore
├── db/                                                # Database directory (Git-excluded)
│   └── csharp-playground.db
├── docs/                                              # Documentation
│   ├── re-organize-project.md                        # This file
│   ├── optimization-wiht-about-me.md                 # Token optimization guide
│   └── architecture-decisions.md                     # (To be created) ADRs
├── .gitignore                                         # Root-level VCS exclusions
├── CLAUDE.md                                          # Claude Code guidance
├── README.md                                          # Project overview
└── csharp-playground.sln                              # Solution file (.NET 8)
```

---

## Key Architectural Improvements

### 1. **Separation of Concerns**
- **Program.cs**: Application orchestration and flow control
- **Data layer**: SchoolContext (EF Core) and Repository pattern abstractions
- **Features/DataAccess**: Data access implementations (IStudentRepository, StudentEFRepository, StudentDapperRepository)
- **Models**: Pure entity definitions with no business logic

### 2. **Repository Pattern Implementation**
```csharp
public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(int id);
}
```
This abstraction allows seamless switching between EF Core and Dapper implementations.

### 3. **Dependency Injection in Program.cs**
```csharp
// Program.cs will configure:
builder.Services.AddScoped<IStudentRepository, StudentEFRepository>();
// Or: builder.Services.AddScoped<IStudentRepository, StudentDapperRepository>();
```

### 4. **Unit Test Organization**
- **Unit tests**: Test individual components in isolation (mocked dependencies)
- **Integration tests**: Test with real SQLite database using shared fixtures
- **Test naming**: `[Feature]Tests.cs` (e.g., `StudentEFRepositoryTests.cs`)

### 5. **Configuration Management**
- Connection strings in `appsettings.json`
- Environment-specific files: `appsettings.Development.json`, `appsettings.Testing.json`
- No hardcoded credentials in source code

---

## Implementation Steps

### Phase 1: Preparation
1. ✅ **Backup current state** (Git commit current working code)
2. ✅ **Create feature branches** for each component refactoring

### Phase 2: Core Refactoring
1. **Create repository interfaces** in `Features/DataAccess/`
2. **Extract data access logic** from MyEF.cs → StudentEFRepository.cs
3. **Extract data access logic** from MyDapper.cs → StudentDapperRepository.cs
4. **Create DatabaseInitializer.cs** for seeding and schema creation
5. **Refactor Program.cs** to be the central orchestrator

### Phase 3: Testing Infrastructure
1. **Create test project structure** with Unit/ and Integration/ folders
2. **Implement DatabaseFixture.cs** for shared test setup
3. **Add xUnit configuration** (xunit.runner.json)
4. **Write unit tests** for repositories (mock DbContext/IDbConnection)
5. **Write integration tests** (real SQLite database)

### Phase 4: Configuration & Documentation
1. **Add environment-specific appsettings** files
2. **Update .gitignore** to exclude *.db files and test artifacts
3. **Create CLAUDE.md** additions for testing guidelines (if not present)
4. **Document ADRs** (Architecture Decision Records) in docs/

---

## File Migration Plan

### Files to Create
| File | Purpose | Status |
|------|---------|--------|
| `src/CSharpPlayground/Features/DataAccess/IStudentRepository.cs` | Repository interface | 🆕 |
| `src/CSharpPlayground/Features/DataAccess/StudentEFRepository.cs` | EF Core implementation | 🆕 |
| `src/CSharpPlayground/Features/DataAccess/StudentDapperRepository.cs` | Dapper implementation | 🆕 |
| `src/CSharpPlayground/Data/DatabaseInitializer.cs` | DB setup & seeding | 🆕 |
| `tests/CSharpPlayground.Tests/Unit/DataAccess/StudentEFRepositoryTests.cs` | Unit tests for EF | 🆕 |
| `tests/CSharpPlayground.Tests/Unit/DataAccess/StudentDapperRepositoryTests.cs` | Unit tests for Dapper | 🆕 |
| `tests/CSharpPlayground.Tests/Integration/Fixtures/DatabaseFixture.cs` | Shared test DB setup | 🆕 |

### Files to Refactor
| File | Current State | Target State | Status |
|------|---------------|--------------|--------|
| `src/CSharpPlayground/Program.cs` | Minimal | Orchestrator with DI setup | ✏️ |
| `src/CSharpPlayground/Data/SchoolContext.cs` | Basic DbContext | Enhanced with migrations | ✏️ |

### Files to Deprecate
| File | Reason | Action |
|------|--------|--------|
| `MyEF.cs` | Logic moved to StudentEFRepository | Archive or remove |
| `MyDapper.cs` | Logic moved to StudentDapperRepository | Archive or remove |

---

## Best Practices Alignment

### Microsoft .NET SDK Guidelines
✅ **Proper directory structure**: `/src` for application, `/tests` for tests  
✅ **Nullable reference types enabled**: `<Nullable>enable</Nullable>`  
✅ **Implicit usings**: Reduces boilerplate code  
✅ **Modern C# features**: required properties, file-scoped types, top-level statements  

### SOLID Principles
✅ **S**ingle Responsibility: Each class has one reason to change  
✅ **O**pen/Closed: Implementations closed for modification, open for extension via interfaces  
✅ **L**iskov Substitution: IStudentRepository implementations are interchangeable  
✅ **I**nterface Segregation: Small, focused repository interface  
✅ **D**ependency Inversion: Depend on abstractions (IStudentRepository), not concrete classes  

### Testing Best Practices
✅ **Isolation**: Unit tests use mocks, integration tests use real DB  
✅ **Repeatability**: Tests run consistently with DatabaseFixture  
✅ **Clear naming**: Test methods follow Arrange-Act-Assert pattern  
✅ **Comprehensive coverage**: Happy path and edge cases  

---

## Configuration Management

### appsettings.json (Development)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=db/csharp-playground.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

### appsettings.Testing.json (Test Environment)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=:memory:"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

---

## Commands for Implementation

### Build & Run
```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the application
dotnet run --project src/CSharpPlayground/CSharpPlayground.csproj

# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "StudentEFRepositoryTests"

# Run with verbose output
dotnet test -v detailed
```

### Database Operations
```bash
# Add migration
dotnet ef migrations add InitialCreate --project src/CSharpPlayground

# Update database
dotnet ef database update --project src/CSharpPlayground

# Drop and recreate (development only)
dotnet ef database drop --project src/CSharpPlayground
```

---

## Success Criteria

After reorganization, the project should satisfy:

- ✅ Clear separation between application, data access, and test layers
- ✅ Repository pattern enables swapping EF Core ↔ Dapper without changing Program.cs
- ✅ Unit tests achieve 80%+ code coverage for data access layer
- ✅ Integration tests validate against real SQLite database
- ✅ All commands in CLAUDE.md execute successfully
- ✅ No breaking changes to public API (backward compatible)
- ✅ Documentation reflects new structure and patterns

---

## References

- [Microsoft .NET SDK Documentation](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [xUnit Testing Framework](https://xunit.net/)
- [Dapper - A light ORM for .NET](https://github.com/DapperLib/Dapper)
