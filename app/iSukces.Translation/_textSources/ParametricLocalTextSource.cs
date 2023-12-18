using System.ComponentModel;
using System.Diagnostics;

namespace iSukces.Translation;

public sealed class ParametricLocalTextSource : TranslationNotifyPropertyChangedBase, ILocalTextSource
{
    public ParametricLocalTextSource(ILocalTextSource item, params object[] args)
    {
        _item         = item;
        _args         = args;
        IsLocalizable = item.IsLocalizable;
        if (_item is INotifyPropertyChanged npc)
            npc.PropertyChanged += NpcOnPropertyChanged;
        UpdateValue();
    }


    private void NpcOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Value))
            UpdateValue();
    }

    private void UpdateValue()
    {
        var value = _item?.Value;
        if (value is null)
        {
            Value = null;
            return;
        }

        try
        {
            Value = string.Format(value, _args);
        }
        catch
        {
            Debug.WriteLine(_item.Value);
            throw;
        }
    }

    public bool IsLocalizable { get; }

    public string Value
    {
        get => _value;
        private set => SetAndNotify(ref _value, value);
    }

    private readonly ILocalTextSource _item;
    private readonly object[] _args;

    private string _value;
}
