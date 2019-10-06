using System.Collections.Generic;
using System;
using System.Linq;


namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var bigrams = new Dictionary<string, int>();
            var trigrams = new Dictionary<string, int>();
            var mostOftenNGrams = new Dictionary<string, string>();
            for (var i = 0; i < text.Count; i++)
                for (var j = 0; j < text[i].Count - 1; j++)
                {
                    if (!bigrams.ContainsKey(text[i][j] + " " + text[i][j + 1]))
                        bigrams.Add(text[i][j] + " " + text[i][j + 1], 1);
                    else
                        bigrams[text[i][j] + " " + text[i][j + 1]] += 1;
                }

            for (var i = 0; i < bigrams.Count; i++)
                for (var j = 0; j < bigrams.Count; j++)
                    if (i != j
                        && bigrams.Keys.ToArray()[i].Split()[0] == bigrams.Keys.ToArray()[j].Split()[0]
                        && (bigrams.Values.ToArray()[i] > bigrams.Values.ToArray()[j] 
                        || bigrams.Values.ToArray()[i] == bigrams.Values.ToArray()[j] 
                        && string.CompareOrdinal(bigrams.Keys.ToArray()[i].Split()[1], bigrams.Keys.ToArray()[j].Split()[1]) > 0))
                        bigrams.Remove(bigrams.Keys.ToArray()[j]);
            foreach (var e in bigrams)
                Console.WriteLine(e);

            return new Dictionary<string, string>();
        }
    }
}