using System.Collections.Generic;
using System.Linq;

public static class Extention
{
    public static IEnumerable<TSource> TakeLast<TSource>(this IEnumerable<TSource> source, int count)
    {
        return source.TakeLast(count);
    }
}
