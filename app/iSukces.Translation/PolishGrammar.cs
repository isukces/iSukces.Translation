namespace iSukces.Translation;

public static class PolishGrammar
{
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