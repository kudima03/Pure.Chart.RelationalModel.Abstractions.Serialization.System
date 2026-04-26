using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System;

internal sealed record AxisRelationalModelJsonModel : IAxisRelationalModel
{
    public AxisRelationalModelJsonModel(IAxisRelationalModel model)
        : this(model.Id, model.Legend) { }

    public AxisRelationalModelJsonModel(IGuid id, IString legend)
    {
        Id = id;
        Legend = legend;
    }

    public IGuid Id { get; }

    public IString Legend { get; }
}

public sealed class AxisRelationalModelConverter : JsonConverter<IAxisRelationalModel>
{
    public override IAxisRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<AxisRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IAxisRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new AxisRelationalModelJsonModel(value),
            options
        );
    }
}
