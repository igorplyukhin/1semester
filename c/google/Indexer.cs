using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly Dictionary<string, Dictionary<int, List<int>>> data =
            new Dictionary<string, Dictionary<int, List<int>>>();

        private readonly Dictionary<int, string> allDocs = new Dictionary<int, string>();

        private readonly char[] delimiters = {' ', '.', ',', '!', '?', ':', '-', '\r', '\n'};

        private void SetDict(string word, int id, int position)
        {
            if (data.ContainsKey(word))
            {
                if (data[word].ContainsKey(id))
                    data[word][id].Add(position - word.Length);
                else
                    data[word].Add(id, new List<int> {position - word.Length});
            }
            else
                data.Add(word, new Dictionary<int, List<int>> {{id, new List<int> {position - word.Length}}});
        }

        public void Add(int id, string documentText)
        {
            var buffer = "";
            if (!allDocs.ContainsKey(id))
                allDocs.Add(id, documentText);
            for (var i = 0; i < documentText.Length; i++)
            {
                var ch = documentText[i];
                if (delimiters.Contains(ch) && buffer != "")
                {
                    SetDict(buffer, id, i);
                    buffer = "";
                }
                else if (Char.IsLetter(ch))
                    buffer += ch;
            }

            if (buffer.Length > 0)
                SetDict(buffer, id, documentText.Length);
        }

        public List<int> GetIds(string word)
            => data.ContainsKey(word)
                ? data[word].Keys.ToList()
                : new List<int>();


        public List<int> GetPositions(int id, string word)
            => data.ContainsKey(word) && data[word].ContainsKey(id)
                ? data[word][id]
                : new List<int>();


        public void Remove(int id)
        {
            var text = allDocs[id].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in text)
            {
                if (data.ContainsKey(word))
                    data[word].Remove(id);
            }
        }
    }
}