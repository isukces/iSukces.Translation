namespace iSukces.Translation;

public interface ILocalTextSource
{
    bool IsLocalizable { get; }

    /// <summary>
    ///     Bieżące tłumaczenie
    /// </summary>
    string Value { get; }
}

public static class LocalTextSourceExt
{
    public static string Format(this ILocalTextSource src, params object[] args) => string.Format(src.Value ?? string.Empty, args);

    public static ILocalTextSource WithParameters(this ILocalTextSource src, params object[] args)
        => new ParametricLocalTextSource(src, args);
}
