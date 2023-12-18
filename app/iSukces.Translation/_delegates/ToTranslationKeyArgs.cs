namespace iSukces.Translation;

public abstract class ToTranslationKeyArgs
{
    public ToTranslationKeyArgs(TranslationKey originalKey) => OriginalKey = originalKey;

    public TranslationKey  OriginalKey { get; }
    public TranslationKey? Key         { get; set; }
}

public abstract class ToTranslationKeyArgs<T> : ToTranslationKeyArgs
{
    protected ToTranslationKeyArgs(TranslationKey originalKey, T source)
        : base(originalKey) => Source = source;

    public T Source { get; }
}
