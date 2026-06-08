using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System.Tests;

public sealed record ChartRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public ChartRelationalModelConverterTests()
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
        RandomString title = new RandomString(new Char('a'), new Char('z'));
        RandomString description = new RandomString(new Char('a'), new Char('z'));
        Guid typeId = new Guid();
        Guid xAxisId = new Guid();
        Guid yAxisId = new Guid();

        IChartRelationalModel chart = new ChartRelationalModel(
            id,
            title,
            description,
            typeId,
            xAxisId,
            yAxisId
        );

        string serialized = JsonSerializer.Serialize(chart, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Title": "{{title.TextValue}}",
              "Description": "{{description.TextValue}}",
              "TypeId": "{{typeId.GuidValue}}",
              "XAxisId": "{{xAxisId.GuidValue}}",
              "YAxisId": "{{yAxisId.GuidValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        RandomString title = new RandomString(new Char('a'), new Char('z'));
        RandomString description = new RandomString(new Char('a'), new Char('z'));
        Guid typeId = new Guid();
        Guid xAxisId = new Guid();
        Guid yAxisId = new Guid();

        IChartRelationalModel expected = new ChartRelationalModel(
            id,
            title,
            description,
            typeId,
            xAxisId,
            yAxisId
        );

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Title": "{{title.TextValue}}",
              "Description": "{{description.TextValue}}",
              "TypeId": "{{typeId.GuidValue}}",
              "XAxisId": "{{xAxisId.GuidValue}}",
              "YAxisId": "{{yAxisId.GuidValue}}"
            }
            """;

        Assert.True(
            new ChartRelationalModelHash(expected).SequenceEqual(
                new ChartRelationalModelHash(
                    JsonSerializer.Deserialize<IChartRelationalModel>(input, _options)!
                )
            )
        );
    }

    [Fact]
    public void RoundTrip()
    {
        IChartRelationalModel chart = new ChartRelationalModel(
            new Guid(),
            new RandomString(new Char('a'), new Char('z')),
            new RandomString(new Char('a'), new Char('z')),
            new Guid(),
            new Guid(),
            new Guid()
        );

        IChartRelationalModel deserialized =
            JsonSerializer.Deserialize<IChartRelationalModel>(
                JsonSerializer.Serialize(chart, _options),
                _options
            )!;

        Assert.True(
            new ChartRelationalModelHash(chart).SequenceEqual(
                new ChartRelationalModelHash(deserialized)
            )
        );
    }
}
