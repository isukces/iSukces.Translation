namespace iSukces.Translation;

/// <summary>
/// Provides utility methods related to Polish grammar rules, particularly for handling
/// cases where the grammatical form of a word depends on a number.
/// </summary>
public static class PolishGrammar
{
    /// <summary>
    /// Determines the correct grammatical form of a noun in Polish based on the given number.
    /// </summary>
    /// <param name="number">The number that determines the grammatical form of the noun.</param>
    /// <param name="form1">The singular form of the noun used when the number equals 1.</param>
    /// <param name="form2">The plural form of the noun used for certain cases (e.g., numbers ending in 2, 3, or 4 without exceptions).</param>
    /// <param name="form3">The plural form of the noun used for other cases, including larger numbers and exceptions.</param>
    /// <returns>The correct grammatical form of the noun based on the given number.</returns>
    public static string NounForm(int number, string form1, string form2, string form3)
    {
        if (number == 1)
            return form1;
        var numberMod100 = number % 100;
        if (numberMod100 < 2 && number > 0)
            return form3;
        if (numberMod100 is >= 10 and < 20)
            return form3;
        numberMod100 %= 10;
        return numberMod100 is <= 1 or > 4 ? form3 : form2;
    }
}