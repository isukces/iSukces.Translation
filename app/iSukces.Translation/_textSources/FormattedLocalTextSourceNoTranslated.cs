using System;
using System.Runtime.CompilerServices;

namespace iSukces.Translation
{
    public sealed class FormattedLocalTextSourceNoTranslated : AbstractCombinedTextSource, ILocalTextSource, IDisposable
    {
        public FormattedLocalTextSourceNoTranslated(string format, params ILocalTextSource[] parameters)
            : base(parameters)
        {
            _format = format;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj); // || obj is FormattedLocalTextSourceNoTranslated other && Equals(other);
        }

        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }

        protected override string GetCurrentValue(ILocalTextSource[] parameters)
        {
            var args = parameters.MapToArray(a => (object)a.Value);
            return string.Format(_format, args);
        }


        private readonly string _format;
    }
}