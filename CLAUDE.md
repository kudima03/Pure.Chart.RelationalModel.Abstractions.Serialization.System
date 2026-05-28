# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet build --no-restore -warnaserror
dotnet format --verify-no-changes             # check code style (CI enforces this)
dotnet format                                  # auto-fix code style
dotnet test --no-build --verbosity normal      # run xUnit tests
dotnet pack --configuration Release -p:PackageVersion=<version> --output .
```

## Architecture

This is a **JSON serialization library** — no domain logic, just `System.Text.Json` converter implementations for chart relational model interfaces.

**Public API:**

| Class | Role |
|---|---|
| `ChartRelationalModelAbstractionsConverters` | Sealed record implementing `IEnumerable<JsonConverter>`; yields all four converters |
| `ChartRelationalModelConverter` | `JsonConverter<IChartRelationalModel>` |
| `ChartSeriesRelationalModelConverter` | `JsonConverter<IChartSeriesRelationalModel>` |
| `AxisRelationalModelConverter` | `JsonConverter<IAxisRelationalModel>` |
| `ChartTypeRelationalModelConverter` | `JsonConverter<IChartTypeRelationalModel>` |

**Internal pattern:** Each converter pairs with an internal sealed record (e.g. `ChartRelationalModelJsonModel`) that implements the same interface and carries `[JsonConstructor]`. The converter reads into the record for deserialization and wraps the interface value in the record for serialization.

**Dependency:** `Pure.Chart.RelationalModel.Abstractions` defines the four interfaces (`IChartRelationalModel`, `IChartSeriesRelationalModel`, `IAxisRelationalModel`, `IChartTypeRelationalModel`). This package provides the `System.Text.Json` serialization layer on top of those abstractions.

**Multi-targeting:** net7.0, net8.0, net9.0, net10.0. `IsAotCompatible` is `false` because the converters rely on reflection-based `System.Text.Json` behaviour.

**Package validation:** `EnablePackageValidation = true` with `PackageValidationBaselineVersion = 0.1.0-preview.1.0.0`. Breaking API changes fail the build.

**Publishing:** triggered by pushing a semver tag (pattern `*.*.*`). The tag becomes the `PackageVersion`.

**Tests:** xUnit project at `src/Tests/Pure.Chart.RelationalModel.Abstractions.Serialization.System.Tests/`. CI runs tests, collects code coverage, and runs Stryker mutation testing on every PR to `main`.

## Code Style

Enforced via `.editorconfig` and `dotnet format --verify-no-changes` in CI:

- No `var` — always use explicit types
- No expression-bodied methods or constructors — block bodies only
- Expression-bodied properties are allowed
- Accessibility modifiers required on all members

## Commit Messages

Do not mention Claude or AI assistance in commit messages.
