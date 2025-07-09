using Xunit;

namespace iSukces.Translation.Test;

public class PolishGrammarTests
{
    [Theory]
    [InlineData(1, "patyk")]
    [InlineData(2, "patyki")]
    [InlineData(3, "patyki")]
    [InlineData(4, "patyki")]
    [InlineData(5, "patyków")]
    [InlineData(10, "patyków")]
    [InlineData(11, "patyków")]
    [InlineData(12, "patyków")]
    [InlineData(13, "patyków")]
    [InlineData(14, "patyków")]
    [InlineData(15, "patyków")]
    [InlineData(16, "patyków")]
    [InlineData(17, "patyków")]
    [InlineData(18, "patyków")]
    [InlineData(19, "patyków")]
    [InlineData(20, "patyków")]
    [InlineData(21, "patyków")]
    [InlineData(30, "patyków")]
    [InlineData(100, "patyków")]
    [InlineData(101, "patyków")]
    [InlineData(102, "patyki")]
    [InlineData(104, "patyki")]
    [InlineData(105, "patyków")]
    [InlineData(200, "patyków")]
    [InlineData(300, "patyków")]
    [InlineData(311, "patyków")]
    [InlineData(320, "patyków")]
    [InlineData(321, "patyków")]
    [InlineData(322, "patyki")]
    [InlineData(324, "patyki")]
    [InlineData(331, "patyków")]
    [InlineData(332, "patyki")]
    [InlineData(333, "patyki")]
    [InlineData(334, "patyki")]
    [InlineData(335, "patyków")]
    [InlineData(336, "patyków")]
    [InlineData(337, "patyków")]
    [InlineData(338, "patyków")]
    [InlineData(339, "patyków")]
    [InlineData(340, "patyków")]
    [InlineData(341, "patyków")]
    [InlineData(351, "patyków")]
    [InlineData(361, "patyków")]
    [InlineData(400, "patyków")]
    [InlineData(500, "patyków")]
    [InlineData(600, "patyków")]
    [InlineData(700, "patyków")]
    [InlineData(800, "patyków")]
    [InlineData(900, "patyków")]
    [InlineData(1000, "patyków")]
    [InlineData(1100, "patyków")]
    public void T01_Should_find_noun(int number, string expected)
    {
       var noun =PolishGrammar.NounForm(number, "patyk", "patyki", "patyków");
       Assert.Equal(expected, noun);
    }
}
