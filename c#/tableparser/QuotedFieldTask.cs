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
        [TestCase("abcd", 0, "abcd", 4)]
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }

        // Добавьте свои тесты
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var value = new StringBuilder();
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '\'' || line[i] == '\"')
                {
                    while (line[i] != line[0])
                    {
                        value.Append(line[i]);
                        i++;
                    }
                }
            }

            return new Token(value.ToString(), startIndex + 1, line.Length - startIndex);
        }
    }
}