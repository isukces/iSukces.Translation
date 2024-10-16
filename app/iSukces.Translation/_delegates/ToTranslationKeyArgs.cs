namespace iSukces.Translation;

public abstract class ToTranslationKeyArgs(TranslationKey? originalKey)
{
    public TranslationKey? OriginalKey { get; } = originalKey;
    public TranslationKey? Key         { get; set; }
}

public abstract class ToTranslationKeyArgs<T>(TranslationKey? originalKey, T source) 
    : ToTranslationKeyArgs(originalKey)
{
    public T Source { get; } = source;
}
