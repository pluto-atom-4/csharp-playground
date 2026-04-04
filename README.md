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

- **Data/** - Entity Framework context and database models
- **Models/** - Domain models
- **MyEF.cs** - Entity Framework demonstration
- **MyDapper.cs** - Dapper ORM demonstration

## Technologies

- **.NET 8.0** - Latest stable .NET runtime
- **Entity Framework Core 8.0.25** - Modern ORM framework
- **Dapper 2.1.15** - Lightweight micro-ORM
- **SQLite** - Embedded database for testing

## Running the Project

```bash
dotnet run
```

This will execute both Entity Framework and Dapper demonstrations, creating sample data and querying from the database.

## Database

The project uses SQLite with database file located at `db/csharp-playground.db`.

---

**Assessment Focus**: Evaluating LTS and latest .NET programming patterns and methodologies.

