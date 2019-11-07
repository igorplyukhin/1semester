using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }
        
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
			var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
			var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
			count = Math.Min(phrases.Count - 1 - leftIndex, Math.Min(count, rightIndex - leftIndex - 1));
            var topByPrefix = new string[count];
            for (var i = 0; i < count; i++)
                topByPrefix[i] = phrases[leftIndex + i + 1];

            return topByPrefix;
        }
        
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var leftBoarder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var rightBoarder = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return rightBoarder - leftBoarder - 1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [TestCase(new []{"a", "ab", "abc"}, "a", 3, new []{"a", "ab", "abc"})]
        [TestCase(new []{"a", "ab", "abc"}, "a", 1, new []{"a"})]
        [TestCase(new []{"a", "ab", "abc"}, "a", 0, new string[]{})]
        public void UniversalTopByPrefixTest(string[] nonParsedPhrases, string prefix, int count, string[] expectedTopWords)
        {
            var phrases = nonParsedPhrases.ToList().AsReadOnly();
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedTopWords, actualTopWords);
        }
        
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var phrases = new List<string>{"a"}.AsReadOnly();
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, "b", 10);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new List<string>{"a", "abc", "xyz"}.AsReadOnly();
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "");
            var expectedCount = phrases.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
