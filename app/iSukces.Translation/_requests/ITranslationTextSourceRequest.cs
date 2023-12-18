namespace iSukces.Translation;

public interface ITranslationTextSourceRequest : ITranslationRequest
{
    string GetLanguage();
    string GetSourceTextToTranslate();

    string TranslationHint { get; }
}
