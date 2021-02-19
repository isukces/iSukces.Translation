using System.Linq;
using Xunit;

namespace iSukces.Translation.Test
{
    public class BasicTranslationTests
    {
        [Fact]
        public void T01_Should_read_translations()
        {
            var h = new CascadeTranslationHolder();
            h.AddFromType(typeof(TestingTexts));
            Assert.Equal(2, h.Translations.Count);
            {
                var r = h.TryGetTranslation("MyTexts.Master", out var t);
                Assert.True(r);
                Assert.Equal("Master text", t);
            }
            {
                var r = h.TryGetTranslation("MyTexts.Nested.Slave", out var t);
                Assert.True(r);
                Assert.Equal("Slave text", t);
            }
            {
                var r = h.TryGetTranslation("MyTexts.NotExisting.Slave", out var t);
                Assert.False(r);
            }
        }

        [Fact]
        public void T02_Should_read_translations()
        {
            var translations = TranslationUtils.GetTranslationsFromType(typeof(TestingTexts), true).ToArray();
            Assert.Equal(2, translations.Length);
        }
 
    }
}