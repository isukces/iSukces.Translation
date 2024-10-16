using System;
using System.ComponentModel;

namespace iSukces.Translation;

public abstract class AbstractCombinedTextSource : TranslationNotifyPropertyChangedBase, IDisposable,
    ILocalTextSource
{
    static AbstractCombinedTextSource()
    {
#if NET48_OR_GREATER
        Empty = Array.Empty<ILocalTextSource>();
#else
        Empty = Net48Compatibility.ArrayEmpty<ILocalTextSource>();
#endif
    }

    protected AbstractCombinedTextSource(params ILocalTextSource[]? parameters)
    {
        _parameters = parameters ?? Empty;
        for (var index = _parameters.Length - 1; index >= 0; index--)
        {
            var i = _parameters[index];
            if (i is INotifyPropertyChanged c)
                c.PropertyChanged += NestedPropertyChanged;
            if (i.IsLocalizable)
                IsLocalizable = true;
        }
    }

    ~AbstractCombinedTextSource()
    {
        Dispose();
    }

    public void Dispose()
    {
        for (var index = _parameters.Length - 1; index >= 0; index--)
        {
            var i = _parameters[index];
            if (i is INotifyPropertyChanged c)
                c.PropertyChanged -= NestedPropertyChanged;
        }

        _parameters = Empty;
    }

    protected abstract string GetCurrentValue(ILocalTextSource[] parameters);

    private void NestedPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(ILocalTextSource.Value)) return;
        _value = null;
        OnPropertyChanged(nameof(Value));
    }


    public bool IsLocalizable { get; }

    public string Value
    {
        get
        {
            if (_value is not null) 
                return _value;
            _value = GetCurrentValue(_parameters);
            return _value;
        }
    }

    #region Fields

    private ILocalTextSource[] _parameters;
    private string? _value;
    private static readonly ILocalTextSource[] Empty;

    #endregion
}
