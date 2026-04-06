# Token Inspection Task - Quick Reference Card

**Task**: Create IStudentRepository abstraction and extract EF Core implementation  
**Complexity**: Moderate | **Duration**: 15-30 min | **Status**: Ready

---

## The Task in 30 Seconds

Create two new files in `/src/CSharpPlayground/Features/DataAccess/`:

1. **IStudentRepository.cs** - Interface with 5 async CRUD methods
2. **StudentEFRepository.cs** - Extract EF Core logic from MyEF.cs

**Success**: Code compiles with `dotnet build` ✅

---

## Input Files to Read (3)

| File | Size | Purpose |
|------|------|---------|
| `MyEF.cs` | 44 lines | Current EF Core implementation (extract from this) |
| `Data/SchoolContext.cs` | 19 lines | EF Core DbContext (understand constructor injection) |
| `Models/Student.cs` | 10 lines | Student entity (understand model structure) |

---

## Output Files to Create (2)

### 1️⃣ `/src/CSharpPlayground/Features/DataAccess/IStudentRepository.cs`

**Content**: Interface definition
```csharp
namespace CSharpPlayground.Features.DataAccess;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(int id);
}
```

**Size**: ~20-25 lines  
**Import**: `using CSharpPlayground.Models;`

### 2️⃣ `/src/CSharpPlayground/Features/DataAccess/StudentEFRepository.cs`

**Content**: Extract database operations from MyEF.cs
```csharp
// Constructor injection of SchoolContext
// Implement all 5 interface methods
// Extract only data access logic from MyEF.cs
// Remove: configuration loading, directory creation, console output
```

**Size**: ~50-60 lines  
**Imports**: `using CSharpPlayground.Data;`, `using CSharpPlayground.Models;`

---

## Success Criteria Checklist

- [ ] Directory created: `src/CSharpPlayground/Features/DataAccess/`
- [ ] File 1: `IStudentRepository.cs` exists with interface definition
- [ ] File 2: `StudentEFRepository.cs` exists and implements interface
- [ ] Logic extracted from `MyEF.cs` database operations
- [ ] Command succeeds: `dotnet build`
- [ ] No compilation errors or warnings

---

## Constraints (What NOT to do)

```
❌ Do NOT modify MyEF.cs
❌ Do NOT modify MyDapper.cs
❌ Do NOT update Program.cs
❌ Do NOT create or run tests
❌ Do NOT modify appsettings.json
❌ Do NOT change project structure beyond Features/DataAccess/
```

---

## Files NOT to Touch

```
Program.cs                          # Will be refactored in Phase 2, Step 5
MyEF.cs                            # Will be refactored in Phase 2, Step 5
MyDapper.cs                        # Will be refactored in Phase 2, Step 6
csharp-playground.csproj           # No changes needed
Data/SchoolContext.cs              # Read-only (understand structure)
Models/Student.cs                  # Read-only (understand model)
```

---

## Validation Commands

```bash
# Check compilation
dotnet build

# List created files
ls -la src/CSharpPlayground/Features/DataAccess/

# Verify structure
tree src/CSharpPlayground/Features/
# Expected output:
# Features/
# └── DataAccess/
#     ├── IStudentRepository.cs
#     └── StudentEFRepository.cs
```

---

## Expected Tokens

| Condition | Tokens | Notes |
|-----------|--------|-------|
| WITHOUT guidance files | ~6,100 | Baseline context cost |
| WITH guidance files | ~7,200 | Includes CLAUDE.md overhead |
| **Context Overhead** | **~1,100** | **Estimated 15.3%** |

---

## Task Execution Flow

```
1. Read 3 input files (MyEF.cs, SchoolContext.cs, Student.cs)
   ↓
2. Create /src/CSharpPlayground/Features/DataAccess/ directory
   ↓
3. Create IStudentRepository.cs interface
   ↓
4. Create StudentEFRepository.cs with extracted logic
   ↓
5. Validate: dotnet build succeeds
   ↓
✅ DONE
```

---

## Key Concepts to Understand

### Repository Pattern
- Abstracts data access behind an interface
- Allows swapping implementations (EF Core vs Dapper) without changing callers
- Improves testability (can mock for unit tests)

### Dependency Injection
- Constructor receives `SchoolContext` instead of creating it
- Enables loose coupling and easier testing

### Extraction from MyEF.cs
Take THIS:
```csharp
// Mixed concerns in MyEF.cs
db.Database.EnsureCreated();
db.Students.Add(new Student { Name = "Alice" });
db.SaveChanges();
var efStudents = db.Students.ToList();
```

Extract TO:
```csharp
// Clean StudentEFRepository.cs
public async Task AddAsync(Student student)
{
    _context.Students.Add(student);
    await _context.SaveChangesAsync();
}
```

---

## Common Issues & Solutions

| Issue | Solution |
|-------|----------|
| Directory creation fails | Check path: `src/CSharpPlayground/Features/DataAccess/` |
| Compile errors (missing `using`) | Add: `using CSharpPlayground.Models;` |
| Interface method signature wrong | Verify all 5 methods return `Task<...>` and are async |
| SchoolContext constructor issue | Use constructor injection: `public StudentEFRepository(SchoolContext context)` |
| Build still fails | Review MyEF.cs extraction - only extract data access, not config |

---

## After Task Completion

1. ✅ Record metrics in `inspection-execution-check-list.md` template
2. ✅ Run second session WITHOUT guidance files
3. ✅ Compare token usage between sessions
4. ✅ Document findings in `docs/token-analysis-results.json`
5. ✅ Proceed to Phase 2, Step 3 (Extract Dapper implementation)

---

## References

- Full task details: `docs/inspection-execution-check-list.md`
- Reorganization context: `docs/re-organize-project.md` (Phase 2)
- Token analysis guide: `docs/optimization-wiht-about-me.md`
- Project analysis: `docs/inspection-about-me.md`

---

**Ready to Start?** Copy the prompt from `inspection-execution-check-list.md` under "PROMPT TO USE IN CLAUDE CODE" and paste it into Claude Code.

