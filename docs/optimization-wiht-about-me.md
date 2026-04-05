## Background

This guide provides instructions to inspect token consumption by comparing Claude Code collaboration sessions with and without `about-me.md` (or the equivalent global `~/.claude/claude.md` file).

To inspect token consumption and compare the impact of project-specific and global guidance files in Claude Code, you can use built-in slash commands or external CLI tools that parse local session logs.

### Reference Resources
- [How to Track Claude Code Usage + Analytics - Shipyard.build](https://shipyard.build/blog/claude-code-track-usage/)
- [Manage Costs Effectively - Claude Code Docs - Shipyard.build](https://shipyard.build/blog/claude-code-tokens)
- [I Built a Local Dashboard to Inspect Claude Code Sessions - Reddit Discussion](https://www.reddit.com/r/Anthropic/comments/1sabaop/i_built_a_local_dashboard_to_inspect_claude_code/)

### Method 1: Built-in Slash Commands (Current Session)
These commands provide the fastest way to see how much context is being consumed in real-time during a single session.

1. **Establish baseline with guidance files**:
   - Ensure `CLAUDE.md` is present in the project root
   - Ensure `~/.claude/about-me.md` exists globally (create if needed)
   - Start a fresh Claude Code session

2. **Run initial task**:
   - Define a specific, repeatable task (e.g., "Implement feature X with unit tests")
   - Execute the task and track the number of messages and tool calls

3. **Inspect context usage**:
   - After completing the task, type `/context` to view the breakdown
   - Note these metrics:
     - **Total tokens used**: Sum of input and output
     - **System tokens**: Weight of CLAUDE.md, about-me.md, and system instructions
     - **User tokens**: Your prompts and file attachments
     - **Assistant tokens**: Response content

4. **Establish control (without guidance files)**:
   - Type `/clear` to reset context
   - Temporarily rename or move your guidance files:
     ```bash
     mv ~/.claude/about-me.md ~/.claude/about-me.md.bak
     # Or in project: rename CLAUDE.md → CLAUDE.md.bak
     ```
   - **Start a new session** (important: new sessions don't inherit from previous ones)

5. **Repeat the same task** without guidance files and compare results

6. **Restore files**:
   ```bash
   mv ~/.claude/about-me.md.bak ~/.claude/about-me.md
   ```

### Method 2: Use CLI Analysis Tools (Session History)
For a more detailed comparison of multiple sessions, use community-built tools that read the JSONL log files stored in `~/.claude/`.

#### ccusage - Token Analysis by Session
```bash
# Install ccusage
npm install -g ccusage

# View token consumption across all sessions
npx ccusage@latest

# Compare specific sessions
# Look for sessions with matching task types and count the difference in "Input Tokens"
```

**What to look for:**
- Sessions run WITH `CLAUDE.md` + `about-me.md` should show higher system token usage
- Sessions run WITHOUT guidance files should show lower initial overhead
- Sessions with many tool calls will show proportionally higher costs

#### cc-lens - Visual Dashboard
```bash
# Clone and run the visualization dashboard
git clone https://github.com/[cc-lens-repo].git
cd cc-lens
npm install && npm start

# Browse your local session history with interactive charts
```

**Benefits:**
- Visual breakdown of token consumption by message
- Tool call pattern analysis
- Session comparison side-by-side


### Token Consumption Breakdown

| Feature       | Typical Token Cost                  | Persistence           | Optimization Notes |
|:--------------|:------------------------------------|:----------------------|:-------------------|
| System Prompt | Baseline cost for every message     | Always loaded         | Fixed; no control |
| `CLAUDE.md`     | ~1,900+ tokens (depending on size)  | Per project           | Remove unused sections; keep concise |
| `about-me.md`   | Variable (your text length)         | Global (all projects) | Applies to ALL sessions; minimize size |
| Tool Calls    | High overhead (2-3x text tokens)    | Per task              | Reduce file I/O; batch operations |

**Pro Tips:**
1. **Session Reuse**: Tokens are cached within the same conversation; subsequent messages cost less
2. **File Attachments**: Attaching files costs more than referencing them; prefer references when possible
3. **Stateless Design**: Each new Claude Code session must reload all context; keep guidance files lean
4. **Batch Operations**: Group multiple related changes into single tool calls to reduce overhead

---

## Pre-Inspection Checklist

Before running your token inspection, prepare and track these items:

- [ ] **Establish baseline metrics**:
  - Size of `CLAUDE.md` (current bytes/lines)
  - Size of `~/.claude/about-me.md` (if exists)
  - Number of sections in each file
  
- [ ] **Define your test task**:
  - Specific task (e.g., "Implement StudentRepository with unit tests")
  - Estimated complexity (simple/moderate/complex)
  - Planned tool calls (file reads, edits, terminal runs)

- [ ] **Prepare test environment**:
  - Clear old sessions: `/clear` in Claude Code or rename `~/.claude/` cache
  - Backup guidance files before renaming them
  - Note the exact time of each test run

- [ ] **Document test results**:
  - Session ID from each run
  - Total tokens consumed (input + output)
  - System tokens specifically
  - Number of messages exchanged
  - Number of tool calls executed
  - Time to completion

- [ ] **Perform comparison**:
  - Run WITH all guidance files → Record metrics
  - Rename/remove guidance files → Start fresh session
  - Run SAME task WITHOUT guidance files → Record metrics
  - Calculate difference: `With - Without = Context Overhead`

- [ ] **External validation**:
  - Run `npx ccusage@latest` to cross-check session logs
  - Export session data for analysis
  - Store results in `docs/token-analysis-results.json` for tracking

## Analysis Interpretation Guide

### Metrics to Compare

**Session WITH Guidance Files:**
- Total tokens: [HIGH]
- System tokens: [BASELINE + CLAUDE.md + about-me.md]
- Messages: [N]
- Tool calls: [M]

**Session WITHOUT Guidance Files:**
- Total tokens: [LOWER]
- System tokens: [BASELINE only]
- Messages: [N] (same as above)
- Tool calls: [M] (same as above)

**Calculation:**
```
Context Overhead = (Tokens_WITH - Tokens_WITHOUT) / Tokens_WITHOUT * 100%
Avg Cost per Message_WITH = Total_Tokens_WITH / Messages
Avg Cost per Message_WITHOUT = Total_Tokens_WITHOUT / Messages
```

### Interpretation Examples

| Scenario | System Tokens Diff | Recommendation |
|----------|-------------------|-----------------|
| < 5% overhead | ✅ Keep all guidance files | Context is valuable and well-sized |
| 5-15% overhead | ⚠️ Review and optimize | Remove redundant sections |
| > 15% overhead | ❌ Significantly reduce | Too much context; break into smaller files or links |

---

## References

- **[How to Track Claude Code Usage + Analytics - Shipyard.build](https://shipyard.build/blog/claude-code-track-usage/)**  
  Detailed guide on using ccusage CLI to inspect token consumption across sessions with filtering and export options.

- **[Manage Costs Effectively - Claude Code Docs - Shipyard.build](https://shipyard.build/blog/claude-code-tokens)**  
  Best practices for minimizing token usage, including session management, context efficiency, and cost tracking strategies.

- **[I Built a Local Dashboard to Inspect Claude Code Sessions - Reddit Discussion](https://www.reddit.com/r/Anthropic/comments/1sabaop/i_built_a_local_dashboard_to_inspect_claude_code/)**  
  Community-built tool (cc-lens) for visual analysis of session logs with interactive charts and token breakdowns.

