using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Constraints;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        static char[] separator = new[] { ':', '!', '.', '?', '(', ')', ';' };

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            foreach (var sentence in text.Split(separator))
            {
                var words = SentenceToWordList(sentence);
                if (words.Count > 0)
                    sentencesList.Add(words);
            }

            return sentencesList;
        }

        private static List<string> SentenceToWordList(string sentence)
        {
            var wordList = new List<string>();
            var word = new StringBuilder();
            foreach (var ch in sentence.ToLowerInvariant())
            {
                if (char.IsLetter(ch) || ch == '\'')
                    word.Append(ch);
                else if (word.Length > 0)
                {
                    wordList.Add(word.ToString());
                    word.Clear();
                }
            }
            if (word.Length > 0)
                wordList.Add(word.ToString());

            return wordList;
        }
    }
}