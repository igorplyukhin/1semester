using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            foreach (var sentence in text.Split(new[] {':', '!', '.', '?', '(', ')', ';'}))
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
            var isNewWord = true;
            foreach (var e in sentence.ToLowerInvariant())
            {
                if (char.IsLetter(e) || e == '\'')
                    if (isNewWord)
                    {
                        wordList.Add(e.ToString());
                        isNewWord = false;
                    }
                    else
                        wordList[wordList.Count - 1] += e;
                else
                    isNewWord = true;
            }

            return wordList;
        }
    }
}