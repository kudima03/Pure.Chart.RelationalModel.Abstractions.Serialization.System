using System.Collections;
using System.Text.Json.Serialization;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System;

public sealed record ChartRelationalModelAbstractionsConverters
    : IEnumerable<JsonConverter>
{
    public IEnumerator<JsonConverter> GetEnumerator()
    {
        yield return new ChartTypeRelationalModelConverter();
        yield return new ChartSeriesRelationalModelConverter();
        yield return new AxisRelationalModelConverter();
        yield return new ChartRelationalModelConverter();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
