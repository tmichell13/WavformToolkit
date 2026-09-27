# Definition of Done

A work item (story, bug fix, or refactor) is **Done** only when every box is checked.

## Code
- [ ] A failing test was written *before* the production code (red → green → refactor)
- [ ] All tests pass in Test Explorer / `dotnet test`
- [ ] Build produces 0 errors and 0 warnings (includes CS1591 missing XML docs)
- [ ] Public members have XML doc comments (`<summary>`, `<param>`, `<returns>`, `<exception>`)
- [ ] No commented-out code or leftover template files

## Repository hygiene
- [ ] `git status` shows no generated files staged (`.vs/`, `bin/`, `obj/`, `*.user`)
- [ ] Commit message follows Conventional Commits (`test:`, `feat:`, `refactor:`, `docs:`, `chore:`)
- [ ] Each commit builds and passes tests on its own

## Documentation
- [ ] Design doc / diagrams updated if a contract or data flow changed
- [ ] README updated if CLI usage or output changed

## Iteration (checked before tagging a release)
- [ ] All stories planned for the iteration meet the item-level DoD above
- [ ] Release tagged (`v0.1.0`, `v0.2.0`, …) per SemVer
- [ ] Iteration note written (what went well / what didn't / what to change)