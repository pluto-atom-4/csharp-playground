# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

CSharpPlayground is an LTS assessment project exploring modern C# language features and data access patterns (Entity Framework Core vs Dapper). It targets .NET 8 with console-based demonstration code.

## Architecture

Following [Microsoft's official .NET project structure guidance](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props):

```
csharp-playground/
├── src/
│   └── CSharpPlayground/
│       ├── Models/
│       │   └── Student.cs                 # Entity definitions
│       ├── Data/
│       │   └── SchoolContext.cs           # EF Core DbContext
│       ├── Features/
│       │   ├── DataAccess/
│       │   │   ├── MyEF.cs                # EF Core demonstrations
│       │   │   └── MyDapper.cs            # Dapper demonstrations
│       ├── Program.cs                     # Console application entry point
│       ├── appsettings.json               # Configuration (connection strings)
│       └── CSharpPlayground.csproj
├── tests/
│   └── CSharpPlayground.Tests/
│       ├── Unit/                          # Unit test suite
│       └── CSharpPlayground.Tests.csproj
├── db/                                    # SQLite database (excluded from VCS)
├── docs/                                  # Documentation
├── CLAUDE.md
├── README.md
└── csharp-playground.sln
```

**Key architectural decisions:**
- **Separation of concerns**: `src/` contains application code, `tests/` contains test projects
- **Feature-based organization**: Related data access patterns grouped under `Features/DataAccess/`
- **Single responsibility**: Models, Data, and Features folders isolate entity definitions from access patterns
- **Constructor injection**: SchoolContext uses dependency injection pattern for connection strings
- **Comparison orientation**: EF Core (MyEF.cs) and Dapper (MyDapper.cs) implementations kept parallel for pattern evaluation
- **Database isolation**: SQLite file at `db/csharp-playground.db` excluded from version control

## Development Commands

```bash
# Build solution
dotnet build

# Run main application
dotnet run

# Run all unit tests
dotnet test

# Run single test by name
dotnet test --filter "TestMethodName"

# Run tests with verbose output
dotnet test -v detailed

# Run specific test project
dotnet test tests/CSharpPlayground.Tests/CSharpPlayground.Tests.csproj

# Build only (without running)
dotnet build --configuration Release

# Restore dependencies
dotnet restore
```

## Code Patterns & Conventions

- **C# Features**: Leverage C# 12 features including required properties, file-scoped types, and implicit usings
- **EF Core**: Constructor-based DbContext configuration in `src/CSharpPlayground/Data/SchoolContext.cs`
- **Nullable**: Null-safety enabled (`<Nullable>enable</Nullable>` in project files)
- **Implicit Usings**: Global using statements configured per target framework in .csproj
- **Entity Models**: Use `required` keyword for mandatory properties in `src/CSharpPlayground/Models/`
- **Project references**: Test project references main project via relative path in csproj

## Security Practices

- SQLite connection strings are read from appsettings.json (configuration-driven)
- Database files are excluded from version control (.db files in .gitignore)
- No sensitive credentials should be stored in code—use configuration files
- Connection string parameterization is automatic with DbContext/Dapper
- Implicit usings and nullable enabled to reduce attack surface from null reference exploits

## Testing Strategy

Tests use xUnit framework with the following structure:
- Test project: `tests/CSharpPlayground.Tests/CSharpPlayground.Tests.csproj`
- Test organization: Mirror source structure with `tests/CSharpPlayground.Tests/Unit/[Feature]/`
- Test naming: `[Feature]Tests.cs` with `[Fact]` and `[Theory]` attributes
- Fixture support: Create shared test utilities in `tests/CSharpPlayground.Tests/Common/Fixtures/`
- Integration tests: Use `tests/CSharpPlayground.Tests/Integration/` for real database tests (not mocked)

## When Adding Features

1. **Define entity models** in `src/CSharpPlayground/Models/` with `required` keyword for mandatory properties
2. **Update DbContext** in `src/CSharpPlayground/Data/SchoolContext.cs` if adding new DbSets
3. **Create feature folder** under `src/CSharpPlayground/Features/[FeatureName]/` for new data access demonstrations
4. **Add pattern implementations** for both EF Core and Dapper approaches (e.g., `MyEF.cs`, `MyDapper.cs`)
5. **Write tests** in `tests/CSharpPlayground.Tests/Unit/[FeatureName]/` with corresponding test class
6. **Update configuration** in `src/CSharpPlayground/appsettings.json` if new settings are required
7. **Document changes** in `docs/` if the pattern introduces a new architectural decision
