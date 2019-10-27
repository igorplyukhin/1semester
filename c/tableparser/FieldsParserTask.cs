using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("    ", new string[] { })]
        [TestCase("\'\\\\ \'", new[] {"\\ "})]
        [TestCase("\'\\\\\'", new[] {"\\"})]
        [TestCase("text", new[] {"text"})]
        [TestCase("hello world", new[] {"hello", "world"})]
        [TestCase("text 'text'", new[] {"text", "text"})]
        [TestCase("text'text'", new[] {"text", "text"})]
        [TestCase("text 'text", new[] {"text", "text"})]
        [TestCase("'b'c", new[] {"b", "c"})]
        [TestCase("''", new[] {""})]
        [TestCase(" a", new[] {"a"})]
        [TestCase("a  'b'", new[] {"a", "b"})]
        [TestCase("'\"1\" \"2\"'", new[] {"\"1\" \"2\""})]
        [TestCase("\"a 'b'\"", new[] {"a 'b'"})]
        [TestCase("'a'\"b\"", new[] {"a", "b"})]
        [TestCase("\"a\\\"\"", new[] {"a\""})]
        [TestCase("\'a\\\'\'", new[] {"a\'"})]
        [TestCase("", new string[] { })]
        [TestCase("\' ", new[] {" "})]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        private static readonly char[] Splitters = {'\'', '\"', ' '};

        public static List<Token> ParseLine(string line)
        {
            var i = 0;
            var parsedLine = new List<Token>();
            Token token;
            while (i < line.Length)
            {
                if (line[i] == '\'' || line[i] == '\"')
                    token = ReadQuotedField(line, i);
                else if (line[i] == ' ')
                {
                    i = SkipBlankField(line, i);
                    continue;
                }
                else
                    token = ReadField(line, i);

                i = token.GetIndexNextToToken();
                parsedLine.Add(token);
            }

            return parsedLine;
        }

        private static Token ReadField(string line, int startIndex)
        {
            var i = startIndex + 1;
            while (i != line.Length && !Splitters.Contains(line[i]))
            {
                i++;
            }

            return new Token(
                line.Substring(startIndex, i - startIndex),
                startIndex,
                i - startIndex);
        }

        private static int SkipBlankField(string line, int startIndex)
        {
            var i = startIndex + 1;
            while (i != line.Length && line[i] == ' ')
                i++;

            return i;
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}