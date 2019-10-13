using System.Collections.Generic;
using System;
using System.Linq;


namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var allPossibleNgrams = new Dictionary<string, Dictionary<string, int>>();
            for (var sentence = 0; sentence < text.Count; sentence++)
            for (var word = 0; word < text[sentence].Count - 1; word++)
            {
                var currentWord = text[sentence][word];
                var nextWord = text[sentence][word + 1];
                var twoWords = currentWord + ' ' + nextWord;
                AddNGram(allPossibleNgrams, currentWord, nextWord);
                if (word != text[sentence].Count - 2)
                {
                    var wordThroughOne = text[sentence][word + 2];
                    AddNGram(allPossibleNgrams, twoWords, wordThroughOne);
                }
            }

            return GetMostOftenNgrams(allPossibleNgrams);
        }

        private static Dictionary<string, string> GetMostOftenNgrams(
            Dictionary<string, Dictionary<string, int>> allPossibleNgrams)
        {
            var mostOftenNgrams = new Dictionary<string, string>();
            foreach (var firstPart in allPossibleNgrams)
            {
                var sortedPossibleBigrams = firstPart.Value.OrderByDescending(x => x.Value);
                var mostOftenSecondPart = sortedPossibleBigrams.ElementAt(0).Key;
                foreach (var secondPart in sortedPossibleBigrams)
                    if (secondPart.Value == sortedPossibleBigrams.ElementAt(0).Value)
                        if (string.CompareOrdinal(mostOftenSecondPart, secondPart.Key) > 0)
                            mostOftenSecondPart = secondPart.Key;
                        else
                            continue;
                    else
                        break;

                mostOftenNgrams.Add(firstPart.Key, mostOftenSecondPart);
            }

            return mostOftenNgrams;
        }

        private static void AddNGram(Dictionary<string, Dictionary<string, int>> allPossibleNgrams, string firstPart,
            string secondPart)
        {
            if (allPossibleNgrams.ContainsKey(firstPart))
                if (allPossibleNgrams[firstPart].ContainsKey(secondPart))
                    allPossibleNgrams[firstPart][secondPart]++;
                else
                    allPossibleNgrams[firstPart].Add(secondPart, 1);
            else
                allPossibleNgrams.Add(firstPart, new Dictionary<string, int> {{secondPart, 1}});
        }
    }
}