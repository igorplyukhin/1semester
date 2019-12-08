using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly Dictionary<string, Dictionary<int, List<int>>> data =
            new Dictionary<string, Dictionary<int, List<int>>>();

        private readonly Dictionary<int, string> allDocs = new Dictionary<int, string>();

        private static readonly char[] delimiters = {' ', '.', ',', '!', '?', ':', '-', '\r', '\n'};
        
        public void Add(int id, string documentText)
        {
            var buffer = new StringBuilder();
            if (!allDocs.ContainsKey(id))
                allDocs.Add(id, documentText);
            for (var i = 0; i < documentText.Length; i++)
            {
                var ch = documentText[i];
                if (delimiters.Contains(ch))
                {
                    if (buffer.Length == 0)
                        continue;
                    AddNewWord(buffer.ToString(), id, i);
                    buffer.Clear();
                }
                else
                    buffer.Append(ch);
            }

            if (buffer.Length > 0)
                AddNewWord(buffer.ToString(), id, documentText.Length);
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
            foreach (var word in data.Keys)
            {
                data[word].Remove(id);
            }
        }

        private void AddNewWord(string word, int id, int position)
        {
            var entryIndex = position - word.Length;
            if (data.ContainsKey(word))
            {
                if (data[word].ContainsKey(id))
                    data[word][id].Add(entryIndex);
                else
                    data[word].Add(id, new List<int> {entryIndex});
            }
            else
                data.Add(word, new Dictionary<int, List<int>> {{id, new List<int> {entryIndex}}});
        }
    }
}