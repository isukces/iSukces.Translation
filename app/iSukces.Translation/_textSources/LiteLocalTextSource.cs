using System;

namespace iSukces.Translation
{
    public sealed class LiteLocalTextSource : LocalTextSourceBase, IEquatable<LiteLocalTextSource>
    {
        public LiteLocalTextSource(string key) : base(key)
        {
        }

        public static bool operator ==(LiteLocalTextSource left, LiteLocalTextSource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LiteLocalTextSource left, LiteLocalTextSource right)
        {
            return !Equals(left, right);
        }

        public bool Equals(LiteLocalTextSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return OriginalKey == other.OriginalKey;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is LiteLocalTextSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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

        protected override string GetCurrentTranslation()
        {
            if (!(TranslationHolder is null) && TranslationHolder.TryGetTranslation(OriginalKey, out var text))
                return text;
            return "??" + OriginalKey;
        }
    }


    partial class LocalTextSource : IEquatable<LocalTextSource>
    {
        public static bool operator ==(LocalTextSource left, LocalTextSource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LocalTextSource left, LocalTextSource right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is LocalTextSource otherCasted && Equals(otherCasted);
        }

        public bool Equals(LocalTextSource other)
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
    }
}