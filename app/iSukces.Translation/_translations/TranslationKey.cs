using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace iSukces.Translation
{
    public sealed class TranslationKey : IEquatable<TranslationKey>, IComparable<TranslationKey>, IComparable
    {
        private TranslationKey([NotNull] string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            // path = path.TrimEnd(' ', '.');
#if DEBUG
            if (path.Contains("..") || path.EndsWith(" ") || path.EndsWith("."))
                throw new Exception("Invalid translation key");
#endif
            Path = path;
        }

        public static TranslationKey Join(TranslationKey[] items)
        {
            TranslationKey root = null;
            if (items is null || items.Length == 0)
                return null;
            for (var index = 0; index < items.Length; index++)
                root += items[index];

            return root;
        }

        public static TranslationKey operator +(TranslationKey x, TranslationKey y)
        {
            if (y is null)
                return x;
            if (x is null || y.IsAbsolute)
                return y;
            return new TranslationKey(x.Path + "." + y.Path);
        }

        public static bool operator ==(TranslationKey left, TranslationKey right)
        {
            return Equals(left, right);
        }

        public static explicit operator TranslationKey(string x)
        {
            x = x?.Trim();
            if (string.IsNullOrEmpty(x))
                return null;
            return new TranslationKey(x);
        }

        public static bool operator >(TranslationKey left, TranslationKey right)
        {
            return Comparer<TranslationKey>.Default.Compare(left, right) > 0;
        }

        public static bool operator >=(TranslationKey left, TranslationKey right)
        {
            return Comparer<TranslationKey>.Default.Compare(left, right) >= 0;
        }

        public static bool operator !=(TranslationKey left, TranslationKey right)
        {
            return !Equals(left, right);
        }

        public static bool operator <(TranslationKey left, TranslationKey right)
        {
            return Comparer<TranslationKey>.Default.Compare(left, right) < 0;
        }

        public static bool operator <=(TranslationKey left, TranslationKey right)
        {
            return Comparer<TranslationKey>.Default.Compare(left, right) <= 0;
        }

        public int CompareTo(TranslationKey other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Path, other.Path, StringComparison.Ordinal);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is TranslationKey other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(TranslationKey)}");
        }

        public bool Equals(TranslationKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Path == other.Path;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is TranslationKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        public TranslationKey IgnoreAbsolute()
        {
            if (IsAbsolute)
                return (TranslationKey)Path.Substring(1);
            return this;
        }

        public override string ToString()
        {
            return Path;
        }

        public TranslationKey TrimPrefix(TranslationKey keyPrefix)
        {
            if (keyPrefix is null)
                return this;
            var path   = IsAbsolute ? Path.Substring(1) : Path;
            var prefix = keyPrefix.Path.TrimEnd('.') + ".";
            path = path.StartsWith(prefix)
                ? path.Substring(prefix.Length)
                : "." + path;
            return (TranslationKey)path;
        }

        [NotNull]
        public string Path { get; }

        public bool IsAbsolute => Path.StartsWith(".");
    }
}