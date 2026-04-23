using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System;

internal sealed record ChartTypeRelationalModelJsonModel : IChartTypeRelationalModel
{
    public ChartTypeRelationalModelJsonModel(IChartTypeRelationalModel model)
        : this(model.Id, model.Name) { }

    [JsonConstructor]
    public ChartTypeRelationalModelJsonModel(IGuid id, IString name)
    {
        Id = id;
        Name = name;
    }

    public IGuid Id { get; }

    public IString Name { get; }
}

public sealed class ChartTypeRelationalModelConverter
    : JsonConverter<IChartTypeRelationalModel>
{
    public override IChartTypeRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<ChartTypeRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IChartTypeRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new ChartTypeRelationalModelJsonModel(value),
            options
        );
    }
}
