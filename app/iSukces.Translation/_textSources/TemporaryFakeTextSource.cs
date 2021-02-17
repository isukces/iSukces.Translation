using System;

namespace iSukces.Translation
{
    public sealed class TemporaryFakeTextSource : ILocalTextSource, IEquatable<TemporaryFakeTextSource>
    {
        public TemporaryFakeTextSource(string value)
        {
            Value = value ?? string.Empty;
        }

        public static bool operator ==(TemporaryFakeTextSource left, TemporaryFakeTextSource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TemporaryFakeTextSource left, TemporaryFakeTextSource right)
        {
            return !Equals(left, right);
        }

        public bool Equals(TemporaryFakeTextSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is FakeTextSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        public string Value { get; }

        public bool IsLocalizable => false;
    }
}