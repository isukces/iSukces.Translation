using System.Diagnostics;
using System.Reflection;

namespace iSukces.Translation
{
    [DebuggerDisplay("Key = {Key}, Value = {Value}")]
    public struct StaticFieldLocalTextSource : ILocalTextSource
    {
        public StaticFieldLocalTextSource(TranslationKey key, FieldInfo field, bool isLocalizable)
        {
            _field        = field;
            Key           = key;
            IsLocalizable = isLocalizable;
        }

        public override string ToString()
        {
            return Value;
        }

        public TranslationKey Key           { get; }
        public bool           IsLocalizable { get; }

        public string Value => (string)_field.GetValue(null);

        private readonly FieldInfo _field;
    }
}