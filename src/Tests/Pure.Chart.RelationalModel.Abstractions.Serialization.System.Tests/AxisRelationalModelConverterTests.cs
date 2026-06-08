using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System.Tests;

public sealed record AxisRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public AxisRelationalModelConverterTests()
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
        RandomString legend = new RandomString(new Char('a'), new Char('z'));

        IAxisRelationalModel axis = new AxisRelationalModel(id, legend);

        string serialized = JsonSerializer.Serialize(axis, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Legend": "{{legend.TextValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        RandomString legend = new RandomString(new Char('a'), new Char('z'));

        IAxisRelationalModel expected = new AxisRelationalModel(id, legend);

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Legend": "{{legend.TextValue}}"
            }
            """;

        Assert.True(
            new AxisRelationalModelHash(expected).SequenceEqual(
                new AxisRelationalModelHash(
                    JsonSerializer.Deserialize<IAxisRelationalModel>(input, _options)!
                )
            )
        );
    }

    [Fact]
    public void RoundTrip()
    {
        IAxisRelationalModel axis = new AxisRelationalModel(
            new Guid(),
            new RandomString(new Char('a'), new Char('z'))
        );

        IAxisRelationalModel deserialized =
            JsonSerializer.Deserialize<IAxisRelationalModel>(
                JsonSerializer.Serialize(axis, _options),
                _options
            )!;

        Assert.True(
            new AxisRelationalModelHash(axis).SequenceEqual(
                new AxisRelationalModelHash(deserialized)
            )
        );
    }
}
