namespace iSukces.Translation;

public static class PolishGrammar
{
    public static string NounForm(int number, string form1, string form2, string form3)
    {
        if (number == 1)
            return form1;
        number %= 100;
        if (number is > 10 and < 20)
            return form3;
        number %= 10;
        return number < 5 ? form2 : form3;
    }
}