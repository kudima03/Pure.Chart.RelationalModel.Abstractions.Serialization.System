# Pure.Chart.RelationalModel.Abstractions.Serialization.System

`System.Text.Json` converters for chart relational model abstractions in the **Pure** ecosystem.

[![.NET build & test](https://github.com/kudima03/Pure.Chart.RelationalModel.Abstractions.Serialization.System/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Chart.RelationalModel.Abstractions.Serialization.System/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.Chart.RelationalModel.Abstractions.Serialization.System/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Chart.RelationalModel.Abstractions.Serialization.System/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.Chart.RelationalModel.Abstractions.Serialization.System)](https://www.nuget.org/packages/Pure.Chart.RelationalModel.Abstractions.Serialization.System)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.Chart.RelationalModel.Abstractions.Serialization.System` provides `System.Text.Json` converters that serialize and deserialize the chart relational model interfaces defined in [`Pure.Chart.RelationalModel.Abstractions`](https://github.com/kudima03/Pure.Chart.RelationalModel.Abstractions/tree/0.1.0-preview.6.0.0). Each interface maps to a custom `JsonConverter<T>` backed by an internal sealed record that carries `[JsonConstructor]` for round-trip fidelity.

## Converters

| Class | Converts |
|---|---|
| `ChartRelationalModelConverter` | `IChartRelationalModel` |
| `ChartSeriesRelationalModelConverter` | `IChartSeriesRelationalModel` |
| `AxisRelationalModelConverter` | `IAxisRelationalModel` |
| `ChartTypeRelationalModelConverter` | `IChartTypeRelationalModel` |
| `ChartRelationalModelAbstractionsConverters` | Enumerable collection of all four converters above |

## Design Principles

- **Interface-preserving** — converters target abstract interfaces, not concrete types, so any implementation serializes transparently.
- **Internal DTOs** — each converter delegates to a private sealed record with `[JsonConstructor]`; no internal types leak into the public API.

## Dependencies

- [`Pure.Chart.RelationalModel.Abstractions`](https://github.com/kudima03/Pure.Chart.RelationalModel.Abstractions/tree/0.1.0-preview.6.0.0) — read-only interfaces for chart, axis, series, and chart-type relational models

## Target Frameworks

- .NET 7
- .NET 8
- .NET 9
- .NET 10

## Installation

```
dotnet add package Pure.Chart.RelationalModel.Abstractions.Serialization.System
```

## Usage

```csharp
JsonSerializerOptions options = new JsonSerializerOptions();

foreach (JsonConverter converter in new ChartRelationalModelAbstractionsConverters())
{
    options.Converters.Add(converter);
}

string json = JsonSerializer.Serialize(chart, options);
IChartRelationalModel deserialized = JsonSerializer.Deserialize<IChartRelationalModel>(json, options)!;
```
