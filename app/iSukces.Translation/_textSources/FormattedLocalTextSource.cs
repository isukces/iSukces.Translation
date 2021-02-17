using System;

namespace iSukces.Translation
{
    public sealed class FormattedLocalTextSource : LocalTextSourceBase, IEquatable<FormattedLocalTextSource>
    {
        public FormattedLocalTextSource(string originalText, string key, params object[] parameters)
            : base(key)
        {
            _parameters = parameters;
        }

        public static bool operator ==(FormattedLocalTextSource left, FormattedLocalTextSource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FormattedLocalTextSource left, FormattedLocalTextSource right)
        {
            return !Equals(left, right);
        }

        public bool Equals(FormattedLocalTextSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return OriginalKey == other.OriginalKey;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is FormattedLocalTextSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return OriginalKey.GetHashCode();
        }

        protected override string GetCurrentTranslation()
        {
            if (!(TranslationHolder is null) && TranslationHolder.TryGetTranslation(OriginalKey, out var text))
                return string.Format(text, _parameters);
            return "??" + OriginalKey;
        }

        private readonly object[] _parameters;
    }
}