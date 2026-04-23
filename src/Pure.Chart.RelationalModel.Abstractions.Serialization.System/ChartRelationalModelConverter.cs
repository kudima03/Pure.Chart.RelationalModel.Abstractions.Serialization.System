using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System;

internal sealed record ChartRelationalModelJsonModel : IChartRelationalModel
{
    public ChartRelationalModelJsonModel(IChartRelationalModel model)
        : this(
            model.Id,
            model.Title,
            model.Description,
            model.TypeId,
            model.XAxisId,
            model.YAxisId
        )
    { }

    public ChartRelationalModelJsonModel(
        IGuid id,
        IString title,
        IString description,
        IGuid typeId,
        IGuid xAxisId,
        IGuid yAxisId
    )
    {
        Id = id;
        Title = title;
        Description = description;
        TypeId = typeId;
        XAxisId = xAxisId;
        YAxisId = yAxisId;
    }

    public IGuid Id { get; }

    public IString Title { get; }

    public IString Description { get; }

    public IGuid TypeId { get; }

    public IGuid XAxisId { get; }

    public IGuid YAxisId { get; }
}

public sealed class ChartRelationalModelConverter : JsonConverter<IChartRelationalModel>
{
    public override IChartRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<ChartRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IChartRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new ChartRelationalModelJsonModel(value),
            options
        );
    }
}
