using System;
using System.Collections.Generic;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.Extensions;

public static class EnumerableExtensions
{
    public static PagedList<TSource> ToPagedList<TSource>(this IEnumerable<TSource> source, bool stillFetchable = false)
    {
        if (source == null)
        {
            throw new ArgumentNullException($"{nameof(source)} cannot be null");
        }

        return new PagedList<TSource>(source, stillFetchable);
    }
}
