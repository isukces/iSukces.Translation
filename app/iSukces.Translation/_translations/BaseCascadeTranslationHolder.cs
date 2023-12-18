using System;

namespace iSukces.Translation;

public class BaseCascadeTranslationHolder : ITranslationHolder
{
    private void CallOnChangeTranslations()
    {
        var handler = OnChangeTranslations;
        handler?.Invoke(this, EventArgs.Empty);
    }

    public bool TryGetTranslation(string fullTransaltionKey, out string translation)
    {
        if (BaseHolder is not null)
            return BaseHolder.TryGetTranslation(fullTransaltionKey, out translation);
        return TryGetTranslationInternal(fullTransaltionKey, out translation);
    }
    
    protected virtual bool TryGetTranslationInternal(string fullTransaltionKey, out string translation)
    {
        translation = "{{" + fullTransaltionKey + "}}";
        return false;
    }

    private void ValueOnOnChangeTranslations(object? sender, EventArgs e)
    {
        CallOnChangeTranslations();
    }

    public ITranslationHolder? BaseHolder
    {
        get => _baseHolder;
        set
        {
            if (ReferenceEquals(_baseHolder, value))
                return;
            if (value is not null)
                value.OnChangeTranslations -= ValueOnOnChangeTranslations;
            _baseHolder = value;
            if (value is not null)
                value.OnChangeTranslations += ValueOnOnChangeTranslations;
            CallOnChangeTranslations();
        }
    }

    public event EventHandler? OnChangeTranslations;

    private ITranslationHolder? _baseHolder;
}
