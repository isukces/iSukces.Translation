using System;

namespace iSukces.Translation;

public interface ITranslationHolder
{
    bool TryGetTranslation(string fullTransaltionKey, out string translation);
    event EventHandler? OnChangeTranslations;
}
