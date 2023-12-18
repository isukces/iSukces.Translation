#if !NET48_OR_GREATER
namespace iSukces.Translation;

public static class Net48Compatibility
{
    private static class EmptyArray<T>
    {
        internal static readonly T[] Value = new T[0];
    }

    public static T[] ArrayEmpty<T>() => EmptyArray<T>.Value;
}

#endif