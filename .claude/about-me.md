# About Me

This file provides context for Claude Code about your development environment and preferences.

## Role & Expertise

- Developer working with modern .NET and C# technologies
- Focused on LTS (Long-Term Support) versions and best practices
- Evaluating data access patterns (ERM vs micro-ORMs)
- Learning and assessing latest C# language features

## Development Preferences

- **Language**: C# / .NET (targeting net8.0 LTS)
- **Database**: SQLite for local development
- **Testing Framework**: xUnit
- **Code Style**: 
  - Nullable reference types enabled
  - Implicit usings preferred
  - Required properties for mandatory fields
  - Modern C# features (file-scoped types, records where appropriate)
- **IDE**: VS Code or Rider
- **Platform**: Linux

## Security & Best Practices

- No secrets or credentials should be stored in code
- Configuration via appsettings.json for connection strings
- Sensitive files excluded from version control (.db, .env files)
- Unit tests should not mock database access—prefer integration tests with real databases
- Code should follow Microsoft .NET design guidelines

## Communication Style

- Direct and concise feedback preferred
- Focus on actionable guidance
- Show code diffs clearly
- Explain architectural decisions
- Flag security concerns immediately

## Project Goals

1. Assess LTS .NET patterns for production readiness
2. Compare Entity Framework Core vs Dapper for different scenarios
3. Establish best practices for future .NET projects
4. Build a reference implementation for modern C# patterns

## Common Tasks

- Running tests: `dotnet test` (all) or `dotnet test --filter "TestName"` (specific)
- Building: `dotnet build`
- Running app: `dotnet run`
- Adding entities: Create in Models/, update SchoolContext, add tests in tests/Unit/
