using System;

namespace iSukces.Translation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
public sealed class TranslateAttribute : Attribute
{
    public TranslateAttribute(string? key = null, string? language = null)
    {
        Key      = key ?? string.Empty;
        Language = language ?? string.Empty;
    }

    private static string CoalesceNullOrWhiteSpace(string first, string second) => string.IsNullOrWhiteSpace(first) ? second : first;

    public static TranslateAttribute? operator |(TranslateAttribute? a, TranslateAttribute? b)
    {
        if (a is null)
            return b;
        if (b is null)
            return a;
        var key      = CoalesceNullOrWhiteSpace(a.Key, b.Key);
        var language = CoalesceNullOrWhiteSpace(a.Language, b.Language);
        return new TranslateAttribute(key, language);
    }

    public string Key      { get; }
    public string Language { get; }
}
