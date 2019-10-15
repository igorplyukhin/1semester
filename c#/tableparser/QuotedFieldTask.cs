using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'abc'", 0, "abc", 5)]
        [TestCase("b'a'd", 1, "a", 3)]
        [TestCase("'abc", 0, "abc", 4)]
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
            var subLine = line.Substring(startIndex);
            var endIndex = subLine.Length - 1;
            for (var i = 1; i < subLine.Length; i++)
            {
                if (subLine[i] == subLine[0])
                {
                    endIndex = i;
                    break;
                }
            }

            return new Token(subLine.Substring(0, endIndex + 1).Trim(subLine[0]), startIndex, endIndex + 1);
        }
    }
}