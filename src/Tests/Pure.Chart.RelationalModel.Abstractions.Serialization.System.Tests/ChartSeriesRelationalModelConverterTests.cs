using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System.Tests;

public sealed record ChartSeriesRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public ChartSeriesRelationalModelConverterTests()
    {
        _options = new JsonSerializerOptions();

        foreach (JsonConverter converter in new PrimitiveConverters())
        {
            _options.Converters.Add(converter);
        }

        foreach (JsonConverter converter in new ChartRelationalModelAbstractionsConverters())
        {
            _options.Converters.Add(converter);
        }

        _options.WriteIndented = true;
        _options.NewLine = "\n";
    }

    [Fact]
    public void Write()
    {
        Guid id = new Guid();
        Guid chartId = new Guid();
        RandomString legend = new RandomString(new Char('a'), new Char('z'));
        RandomString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        RandomString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        IChartSeriesRelationalModel series = new ChartSeriesRelationalModel(
            id,
            chartId,
            legend,
            xAxisSource,
            yAxisSource
        );

        string serialized = JsonSerializer.Serialize(series, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "ChartId": "{{chartId.GuidValue}}",
              "Legend": "{{legend.TextValue}}",
              "XAxisSource": "{{xAxisSource.TextValue}}",
              "YAxisSource": "{{yAxisSource.TextValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        Guid chartId = new Guid();
        RandomString legend = new RandomString(new Char('a'), new Char('z'));
        RandomString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        RandomString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        IChartSeriesRelationalModel expected = new ChartSeriesRelationalModel(
            id,
            chartId,
            legend,
            xAxisSource,
            yAxisSource
        );

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "ChartId": "{{chartId.GuidValue}}",
              "Legend": "{{legend.TextValue}}",
              "XAxisSource": "{{xAxisSource.TextValue}}",
              "YAxisSource": "{{yAxisSource.TextValue}}"
            }
            """;

        Assert.True(
            new ChartSeriesRelationalModelHash(expected).SequenceEqual(
                new ChartSeriesRelationalModelHash(
                    JsonSerializer.Deserialize<IChartSeriesRelationalModel>(input, _options)!
                )
            )
        );
    }

    [Fact]
    public void RoundTrip()
    {
        IChartSeriesRelationalModel series = new ChartSeriesRelationalModel(
            new Guid(),
            new Guid(),
            new RandomString(new Char('a'), new Char('z')),
            new RandomString(new Char('a'), new Char('z')),
            new RandomString(new Char('a'), new Char('z'))
        );

        IChartSeriesRelationalModel deserialized =
            JsonSerializer.Deserialize<IChartSeriesRelationalModel>(
                JsonSerializer.Serialize(series, _options),
                _options
            )!;

        Assert.True(
            new ChartSeriesRelationalModelHash(series).SequenceEqual(
                new ChartSeriesRelationalModelHash(deserialized)
            )
        );
    }
}
