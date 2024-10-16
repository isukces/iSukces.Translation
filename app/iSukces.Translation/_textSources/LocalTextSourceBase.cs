using System;

namespace iSukces.Translation;

public abstract class LocalTextSourceBase : TranslationNotifyPropertyChangedBase, ILocalTextSource
{
    protected LocalTextSourceBase(string key, string value)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));
        OriginalKey = key;
        Value       = value;
    }

    public void Attach(ITranslationHolder translationHolder)
    {
        if (TranslationHolder is not null)
            TranslationHolder.OnChangeTranslations -= TranslationHolderOnOnChangeTranslations;
        TranslationHolder = translationHolder;
        Value             = GetCurrentTranslation();
        if (TranslationHolder is not null)
            TranslationHolder.OnChangeTranslations += TranslationHolderOnOnChangeTranslations;
    }

    protected abstract string GetCurrentTranslation();

    public override int GetHashCode() => OriginalKey.GetHashCode();

    private void TranslationHolderOnOnChangeTranslations(object? sender, EventArgs e)
    {
        Value = GetCurrentTranslation();
    }

    public string OriginalKey { get; }

    protected ITranslationHolder? TranslationHolder { get; private set; }

    public bool IsLocalizable => true;

    /// <summary>
    ///     Bieżące tłumaczenie
    /// </summary>
    public string Value
    {
        get => _value;
        set => SetAndNotify(ref _value, value ?? "");
    }

    private string _value = "";
}
