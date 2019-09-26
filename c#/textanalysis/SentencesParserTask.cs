using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            foreach (var sentence in text.Split(new[] { ':', '!', '.', '?', '(', ')', ';' }))
            {
                if (SentenceToWordList(sentence).Count !=0)
                {
                    sentencesList.Add(SentenceToWordList(sentence));
                }
            }
            return sentencesList;
        }

        public static List<string> SentenceToWordList(string sentence)
        {
            var wordList = new List<string>();
            bool isNewWord = true;
            foreach (var e in sentence.ToLowerInvariant())
            {
                if (char.IsLetter(e) || e == '\'')
                {
                    if (isNewWord)
                    {
                        wordList.Add(e.ToString());
                        isNewWord = false;
                    }
                    else 
                    {
                        wordList[wordList.Count - 1] += e;
                    }
                }
                else
                    isNewWord = true;
            }
            return wordList;
        }
    }
}
