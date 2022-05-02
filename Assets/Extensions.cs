using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
    {
        return source.OrderBy(selector).FirstOrDefault();
    }

    public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
    {
        return source.OrderBy(selector).LastOrDefault();
    }
}