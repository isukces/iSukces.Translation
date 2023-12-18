namespace iSukces.Translation.Test;

[Translate("MyTexts")]
public class TestingTexts
{
    public static string Master = "Master text";

    public class Nested
    {
        public static string Slave = "Slave text";

        [DoNotTranslate] public static string Skip = "Slave text";

        public static int SomeNumber;
    }
}
