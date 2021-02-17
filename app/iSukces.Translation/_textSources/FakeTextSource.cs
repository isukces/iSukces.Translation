using System;

namespace iSukces.Translation
{
    public sealed class FakeTextSource : ILocalTextSource, IEquatable<FakeTextSource>
    {
        public FakeTextSource(string value)
        {
            Value = value ?? string.Empty;
        }

        public static bool operator ==(FakeTextSource left, FakeTextSource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FakeTextSource left, FakeTextSource right)
        {
            return !Equals(left, right);
        }

        public bool Equals(FakeTextSource other)
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