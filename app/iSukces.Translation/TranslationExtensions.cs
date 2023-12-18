using System;
using System.Collections.Generic;

namespace iSukces.Translation;

public static class TranslationExtensions
{
    public static TOut[] MapToArray<TIn, TOut>(this IReadOnlyList<TIn>? src, Func<TIn, TOut> map)
    {
        if (map == null) throw new ArgumentNullException(nameof(map));
        #if NET48_OR_GREATER
        if (src is null) return Array.Empty<TOut>();
        #else
        if (src is null) return Net48Compatibility.ArrayEmpty<TOut>();
        #endif
        var count = src.Count;
#if NET48_OR_GREATER
        if (count == 0) return Array.Empty<TOut>();
#else
        if (count == 0) return Net48Compatibility.ArrayEmpty<TOut>();
#endif
        var result = new TOut[count];
        // ReSharper disable once LoopCanBeConvertedToQuery
        for (var i = 0; i < count; i++)
        {
            var element = src[i];
            var value   = map(element);
            result[i] = value;
        }

        return result;
    }
}
