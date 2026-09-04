# Changelog

All notable changes to Pure.Chart.RelationalModel.Abstractions.Serialization.System are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.1.0.1] — 2026-06-17

### Fixed

- **`AxisRelationalModelConverter`** and **`ChartRelationalModelConverter`** now mark
  their internal JSON model's full constructor with `[JsonConstructor]`, avoiding
  ambiguous constructor resolution during deserialization of
  `IAxisRelationalModel` and `IChartRelationalModel` values.

## [0.1.0-preview.1.0.0] — 2026-04-26

### Changed

- Updated to `Pure.Chart.RelationalModel.Abstractions` 0.1.0-preview.6.0.0.
  **`AxisRelationalModelConverter`** no longer reads or writes a `ChartId`
  property when serializing `IAxisRelationalModel` values, matching the
  removal of `ChartId` from the abstraction.

## [0.1.0-preview.0.1.0] — 2026-04-23

Initial release.

### Added

- `System.Text.Json` converters for the `Pure.Chart.RelationalModel.Abstractions`
  model types: **`ChartTypeRelationalModelConverter`**,
  **`ChartSeriesRelationalModelConverter`**, **`AxisRelationalModelConverter`**,
  and **`ChartRelationalModelConverter`**.
- **`ChartRelationalModelAbstractionsConverters`** — an
  `IEnumerable<JsonConverter>` bundling all four converters for registration
  with `JsonSerializerOptions`.
