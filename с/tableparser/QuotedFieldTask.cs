using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("\'\\\\\'", 0, "\\", 4)]
        [TestCase("\'\\\\ \'", 0, "\\ ", 5)]
        [TestCase("' '", 0, " ", 3)]
        [TestCase("''", 0, "", 2)]
        [TestCase("'a", 0, "a", 2)]
        [TestCase("'abc'", 0, "abc", 5)]
        [TestCase("b'a'd", 1, "a", 3)]
        [TestCase("'a", 0, "a", 2)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var i = startIndex + 1;
            while (i != line.Length && line[i] != line[startIndex] || line[i - 1] == '\\' && line[i - 2] != '\\')
                i++;
            
            var tokenLength = i == line.Length ? i - startIndex : i - startIndex + 1;
            var tokenValueLength = i == line.Length ? tokenLength - 1 : tokenLength - 2;

            return new Token(
                Regex.Unescape(line.Substring(startIndex + 1, tokenValueLength)),
                startIndex,
                tokenLength);
        }
    }
}