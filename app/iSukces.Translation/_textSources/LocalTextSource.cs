using System;

namespace iSukces.Translation;

public sealed class LocalTextSource : LocalTextSourceBase, IEquatable<LocalTextSource>
{
    [Obsolete("Use LocalTextSource(string key, string originalText) instead", true)]
    public LocalTextSource(string originalText, string key)
        : base(key, originalText)
    {
        if (string.IsNullOrEmpty(originalText))
            throw new ArgumentNullException(nameof(originalText));
        OriginalText = originalText;
    }

    public LocalTextSource(string key, string originalText, string? translationHint)
        : base(key, originalText)
    {
        if (string.IsNullOrEmpty(originalText))
            throw new ArgumentNullException(nameof(originalText));
        OriginalText    = originalText;
        TranslationHint = translationHint;
    }

    public static bool operator ==(LocalTextSource left, LocalTextSource right) => Equals(left, right);

    public static bool operator !=(LocalTextSource left, LocalTextSource right) => !Equals(left, right);

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return other is LocalTextSource otherCasted && Equals(otherCasted);
    }

    public bool Equals(LocalTextSource? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IsLocalizable == other.IsLocalizable
               && StringComparer.Ordinal.Equals(OriginalText, other.OriginalText)
               && StringComparer.Ordinal.Equals(TranslationHint, other.TranslationHint)
               && StringComparer.Ordinal.Equals(OriginalKey, other.OriginalKey)
               && StringComparer.Ordinal.Equals(Value, other.Value)
               && Equals(TranslationHolder, other.TranslationHolder);
    }

    protected override string GetCurrentTranslation()
    {
        if (TranslationHolder is not null && TranslationHolder.TryGetTranslation(OriginalKey, out var text))
            return text;
        return OriginalText;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var code = StringComparer.Ordinal.GetHashCode(OriginalText);
            code = (code * 397) ^ StringComparer.Ordinal.GetHashCode(TranslationHint ?? string.Empty);
            code = (code * 397) ^ StringComparer.Ordinal.GetHashCode(OriginalKey);
            code = (code * 397) ^ StringComparer.Ordinal.GetHashCode(Value ?? string.Empty);
            return ((code * 397) ^ (TranslationHolder?.GetHashCode() ?? 0)) * 2 + (IsLocalizable ? 1 : 0);
        }
    }

    public override string ToString()
    {
#if DEBUG
            return "?BAD BINDING? " + Value;
#else
        // awaryjnie zwracam wartość przetłumaczoną co się pojawi w WPF
        return Value;
#endif
    }

    public string OriginalText { get; }

    public string? TranslationHint { get; }
}
