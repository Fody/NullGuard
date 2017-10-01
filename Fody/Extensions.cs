using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static T FirstOrThrow<T>(this IEnumerable<T> list, Func<T, bool> func, string matchDescription)
        where T: class
    {
        var item = list.FirstOrDefault(func);
        if (item != default(T))
        {
            return item;
        }
        throw new Exception("No item found. MatchDescription: " + matchDescription);
    }
}