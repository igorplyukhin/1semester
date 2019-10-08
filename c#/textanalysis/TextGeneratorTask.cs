using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            while (wordsCount > 0)
            {
                var splitedPhrase = phraseBeginning.Split();
                if (splitedPhrase.Length >= 2)
                {
                    var preLastWord = splitedPhrase[phraseBeginning.Split().Length - 2];
                    if (nextWords.ContainsKey(preLastWord + ' ' + splitedPhrase[splitedPhrase.Length - 1]))
                        phraseBeginning += nextWords[preLastWord + ' ' + splitedPhrase[splitedPhrase.Length -1]];
                }
                else if (nextWords.ContainsKey(splitedPhrase[splitedPhrase.Length - 1]))
                    phraseBeginning += nextWords[splitedPhrase[splitedPhrase.Length - 1]];
                else
                    break;
                wordsCount--;
            }
            return phraseBeginning;
        }
    }
}