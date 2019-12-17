using Microsoft.VisualStudio.TestTools.UnitTesting;
using webapi.Services;

namespace PhpChallengeTests
{
    [TestClass]
    public class PhpChallenge70SeriesTests
    {
        [DataRow("AINAK|SLMAN|MUTLI|HTPVS", "ILMA|MAA|TULI|VESI", 3)]
        [DataRow("AA|AA", "A|AA|AAA|AAAA|AAAAA", 5)]
        [DataRow("ABC|HID|GFE", "ABCBABCBABCBABCBABC", 1)]
        [DataTestMethod]
        public void Problem73(string rows, string words, int expected)
        {
            // max words 20, mag width and height 10, word max length 10.
            var sut = new PhpChallengeService();

            var actual = sut.Sanaruudukko(rows, words);

            Assert.AreEqual(expected, actual);
        }
    }
}
