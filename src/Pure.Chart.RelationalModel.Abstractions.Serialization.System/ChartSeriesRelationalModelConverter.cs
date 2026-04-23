using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System;

internal sealed record ChartSeriesRelationalModelJsonModel : IChartSeriesRelationalModel
{
    public ChartSeriesRelationalModelJsonModel(IChartSeriesRelationalModel model)
        : this(
            model.Id,
            model.ChartId,
            model.Legend,
            model.XAxisSource,
            model.YAxisSource
        )
    { }

    [JsonConstructor]
    public ChartSeriesRelationalModelJsonModel(
        IGuid id,
        IGuid chartId,
        IString legend,
        IString xAxisSource,
        IString yAxisSource
    )
    {
        Id = id;
        ChartId = chartId;
        Legend = legend;
        XAxisSource = xAxisSource;
        YAxisSource = yAxisSource;
    }

    public IGuid Id { get; }

    public IGuid ChartId { get; }

    public IString Legend { get; }

    public IString XAxisSource { get; }

    public IString YAxisSource { get; }
}

public sealed class ChartSeriesRelationalModelConverter
    : JsonConverter<IChartSeriesRelationalModel>
{
    public override IChartSeriesRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<ChartSeriesRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IChartSeriesRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new ChartSeriesRelationalModelJsonModel(value),
            options
        );
    }
}
