using System.Collections.Generic;
using System.Linq;

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
                var lastWord = splitedPhrase.Last();
                var prelastWord = splitedPhrase.Length >= 2 ? splitedPhrase[splitedPhrase.Length - 2] : "";
                if (splitedPhrase.Length >= 2 
                    && nextWords.ContainsKey(prelastWord + ' ' + lastWord))
                    phraseBeginning += ' ' + nextWords[prelastWord + ' ' + lastWord];
                else if (nextWords.ContainsKey(lastWord))
                    phraseBeginning += ' ' + nextWords[lastWord];
                else
                    break;
                wordsCount--;
            }
            return phraseBeginning;
        }
    }
}