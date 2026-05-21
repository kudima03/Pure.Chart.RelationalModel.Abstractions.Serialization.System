using System.Collections;

namespace Pure.Chart.RelationalModel.Abstractions.Serialization.System.Tests;

public sealed record ChartRelationalModelAbstractionsConvertersTests
{
    [Fact]
    public void EnumeratesFourConverters()
    {
        Assert.Equal(4, new ChartRelationalModelAbstractionsConverters().Count());
    }

    [Fact]
    public void NonGenericEnumeratorReturnsAllConverters()
    {
        IEnumerable converters = new ChartRelationalModelAbstractionsConverters();

        int count = 0;

        foreach (object _ in converters)
        {
            count++;
        }

        Assert.Equal(4, count);
    }
}
