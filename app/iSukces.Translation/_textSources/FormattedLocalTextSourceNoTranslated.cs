using System;
using System.Runtime.CompilerServices;

namespace iSukces.Translation;

public sealed class FormattedLocalTextSourceNoTranslated : AbstractCombinedTextSource, ILocalTextSource, IDisposable
{
    public FormattedLocalTextSourceNoTranslated(string format, params ILocalTextSource[] parameters)
        : base(parameters)
        => _format = format;

    public override bool Equals(object obj)
        => ReferenceEquals(this, obj); // || obj is FormattedLocalTextSourceNoTranslated other && Equals(other);

    protected override string GetCurrentValue(ILocalTextSource[] parameters)
    {
        var args = parameters.MapToArray(a => (object)a.Value);
        return string.Format(_format, args);
    }

    public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);

    private readonly string _format;
}
