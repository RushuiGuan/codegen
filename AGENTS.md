# Project Scan Notes (2026-02-15)

## What This Repo Does
- This repository is a multi-project .NET code generation framework centered on generating typed clients from ASP.NET Core Web API projects.
- Primary end-user interface is the `codegen` CLI tool (`Albatross.CodeGen.CommandLine`).
- Main generation targets are:
  - C# web clients/proxies
  - TypeScript DTOs and web client services
  - Python DTOs and web clients

## Core Architecture
- `Albatross.CodeGen`: language-agnostic code generation abstractions (`ICodeElement`, `ICodeNode`, `IExpression`, etc.).
- `Albatross.CodeGen.CSharp`, `Albatross.CodeGen.TypeScript`, `Albatross.CodeGen.Python`: language-specific code model/rendering implementations.
- `Albatross.CodeGen.WebClient`: shared Roslyn-based analysis + Web API model layer (controllers, methods, DTO extraction, filtering).
- `Albatross.CodeGen.WebClient.CSharp|TypeScript|Python`: language-specific web client generators.
- `Albatross.CodeGen.CommandLine`: CLI orchestration over compilation, settings, filtering, conversion, and file output.

## How Generation Works (Observed in Command Handlers)
1. Build/load target C# project into Roslyn `Compilation`.
2. Walk syntax trees and semantic models.
3. Discover API controllers (`ApiControllerClassWalker`) and/or DTO/enums (`DtoClassEnumWalker`).
4. Convert symbols into internal models.
5. Convert models to language-specific file declarations.
6. Emit generated files to output directory.

## CLI Commands (from source attributes/docs)
- `csharp web-client`
- `csharp legacy-web-client`
- `python web-client`
- `python dto`
- `typescript web-client`
- `typescript dto`
- `typescript entrypoint`
- `schema csharp|python|typescript`
- internal/debug model commands:
  - `model controller`
  - `model dto`

## Important Settings Behavior
- Shared settings base: `ControllerFilters`, `ControllerMethodFilters`, `DtoEnumFilters`.
- TypeScript settings include endpoint/base client module and namespace/type mappings.
- Python settings include sync/async option (`requests` vs `httpx`) and multiple rename/mapping dictionaries.
- Real examples of settings are under:
  - `Test.Proxy/codegen-settings.json`
  - `Test.WithInterface.Client/codegen-settings.json`
  - `typescript-client/codegen-settings.json`
  - `python-client/codegen/codegen-settings.json`

## Example Consumers / Fixtures
- `Test.WebApi`: fixture API with many controller edge cases (nullable params/returns, route/body/query/header, obsolete, redirects, filtering, etc.).
- `Test.Proxy`, `Test.Client`, `Test.WithInterface.Client`, `Test.Client402.Proxy`: generated C# client outputs and config variants.
- `typescript-client`: Angular workspace showing TypeScript generation flow.
- `python-client`: generated Python DTO/client output + packaging files.
- `Test.CommandLine`: small CLI consuming generated clients.

## Repo Entry Points and Docs
- Root readme: `README.md` (links full docs site).
- Doc source: `docfx_project/articles/*` (intro, architecture, getting-started, examples, configuration).
- Solution file: `codegen.slnx`.
- CLI entry point: `Albatross.CodeGen.CommandLine/Program.cs`.

## Version/Target Observations
- Projects target `net10.0` in current repo state.
- Shared package metadata/versioning in `Directory.Build.props`.

## Handy Regeneration Scripts
- `Test.Proxy/run.ps1`
- `Test.Client/run.ps1`
- `Test.WithInterface.Client/run.ps1`
- `typescript-client/run.ps1`
- `python-client/run.ps1`

## Fast “Re-orient Me” Checklist for Next Session
1. Open `Albatross.CodeGen.CommandLine/Program.cs`.
2. Open command handlers in `Albatross.CodeGen.CommandLine/*CodeGenCommandHandler.cs`.
3. Open `Albatross.CodeGen.WebClient/*` walkers/converters/settings.
4. Check one fixture:
   - API source: `Test.WebApi/Controllers/*`
   - generated output: `Test.Client`, `Test.Proxy`, `typescript-client`, or `python-client`.
5. Run corresponding `run.ps1` script if regeneration is needed.

## Build vs Buy Codegen Checklist

Use this checklist before adopting an external generator or replacing an internal one.

### Strategic Rule
- Treat this repo as core compile-time infrastructure for the ecosystem, not just an HTTP client generator.
- Prefer keeping internal generators when they preserve architecture consistency and remain low-maintenance.

### Decision Gate (all must be true to replace internal codegen)
1. External tool can meet required runtime integration without architectural regression (example: can work with `Albatross.Http` via configuration/templates/adapters).
2. External output achieves at least 90% parity on real controllers/DTOs used in this repo.
3. Remaining gaps are small and maintainable without forking deep internals.
4. Adoption reduces long-term maintenance cost after migration and guardrail tests.
5. External tool does not block or weaken Roslyn compile-time workflows used elsewhere in the ecosystem.

### Evaluation Procedure
1. Pick two representative APIs:
   - one straightforward controller
   - one edge-case-heavy controller (nullable, collections, auth, filters, custom mappings)
2. Generate outputs side by side (current internal generator vs candidate tool).
3. Score parity in:
   - signatures and naming
   - serialization and nullability behavior
   - auth/header/query/route/body handling
   - extensibility and customization points
   - generated code readability and stability
4. Document required customization:
   - simple configuration only
   - template overrides
   - custom adapter/plugin code
5. Decide:
   - keep internal generator (default)
   - adopt external tool for a bounded slice
   - fully replace only if all decision-gate criteria are met

### Operating Model
- Keep core abstractions (`Albatross.CodeGen*`, Roslyn model pipeline) as the foundation.
- Use external generators only at boundaries where interoperability value is higher than customization cost.
- Re-run the checklist when:
  - adding a new language target
  - major runtime dependency changes
  - significant external tool maturity jump
