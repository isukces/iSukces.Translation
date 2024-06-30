using System;

namespace iSukces.Translation;

public sealed class LiteLocalTextSource : LocalTextSourceBase, IEquatable<LiteLocalTextSource>
{
    public LiteLocalTextSource(string key, string? value = null)
        : base(key, value ??"")
    {
    }

    public static bool operator ==(LiteLocalTextSource left, LiteLocalTextSource right) => Equals(left, right);

    public static bool operator !=(LiteLocalTextSource left, LiteLocalTextSource right) => !Equals(left, right);

    public bool Equals(LiteLocalTextSource? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return OriginalKey == other.OriginalKey;
    }

    public override bool Equals(object? obj) 
        => ReferenceEquals(this, obj) || obj is LiteLocalTextSource other && Equals(other);

    protected override string GetCurrentTranslation()
    {
        if (!(TranslationHolder is null) && TranslationHolder.TryGetTranslation(OriginalKey, out var text))
            return text;
        return "??" + OriginalKey;
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString()
    {
#if DEBUG
            return "?BAD BINDING? " + Value;
#else
        // awaryjnie zwracam wartość przetłumaczoną co się pojawi w WPF
        return Value;
#endif
    }
}
