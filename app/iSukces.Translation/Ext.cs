using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace iSukces.Translation
{
    public static class Ext
    {
        [NotNull]
        public static TOut[] MapToArray<TIn, TOut>(this IReadOnlyList<TIn> src, Func<TIn, TOut> map)
        {
            if (src is null) return Array.Empty<TOut>();
            var count = src.Count;
            if (count == 0) return Array.Empty<TOut>();
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
}