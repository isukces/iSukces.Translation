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

}