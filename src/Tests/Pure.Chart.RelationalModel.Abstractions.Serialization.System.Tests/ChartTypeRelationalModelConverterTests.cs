using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System.Tests;

public sealed record ChartTypeRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public ChartTypeRelationalModelConverterTests()
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
        RandomString name = new RandomString(new Char('a'), new Char('z'));

        IChartTypeRelationalModel chartType = new ChartTypeRelationalModel(id, name);

        string serialized = JsonSerializer.Serialize(chartType, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Name": "{{name.TextValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        RandomString name = new RandomString(new Char('a'), new Char('z'));

        IChartTypeRelationalModel expected = new ChartTypeRelationalModel(id, name);

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Name": "{{name.TextValue}}"
            }
            """;

        Assert.True(
            new ChartTypeRelationalModelHash(expected).SequenceEqual(
                new ChartTypeRelationalModelHash(
                    JsonSerializer.Deserialize<IChartTypeRelationalModel>(input, _options)!
                )
            )
        );
    }

    [Fact]
    public void RoundTrip()
    {
        IChartTypeRelationalModel chartType = new ChartTypeRelationalModel(
            new Guid(),
            new RandomString(new Char('a'), new Char('z'))
        );

        IChartTypeRelationalModel deserialized =
            JsonSerializer.Deserialize<IChartTypeRelationalModel>(
                JsonSerializer.Serialize(chartType, _options),
                _options
            )!;

        Assert.True(
            new ChartTypeRelationalModelHash(chartType).SequenceEqual(
                new ChartTypeRelationalModelHash(deserialized)
            )
        );
    }
}
