using System.Globalization;

namespace iSukces.Translation;

public interface INounGrammarForms
{
    string Choose(int number, params string[] forms);

    CultureInfo Culture { get; }
}
