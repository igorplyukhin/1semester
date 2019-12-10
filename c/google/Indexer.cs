using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly Dictionary<string, Dictionary<int, List<int>>> data =
            new Dictionary<string, Dictionary<int, List<int>>>();

        private readonly Dictionary<int, List<string>> allDocs = new Dictionary<int, List<string>>();

        private static readonly char[] delimiters = {' ', '.', ',', '!', '?', ':', '-', '\r', '\n'};

        public void Add(int id, string documentText)
        {
            var buffer = new StringBuilder();
            for (var i = 0; i < documentText.Length; i++)
            {
                var ch = documentText[i];
                if (delimiters.Contains(ch))
                {
                    if (buffer.Length == 0)
                        continue;
                    var word = buffer.ToString();
                    UpdateData(word, id, i);
                    UpdateAllDocs(word,id);
                    buffer.Clear();
                }
                else
                    buffer.Append(ch);
            }

            if (buffer.Length > 0)
            {
                var word = buffer.ToString();
                UpdateData(word, id, documentText.Length);
                UpdateAllDocs(word,id);
            }
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
            var text = allDocs[id];
            foreach (var word in text)
            {
                if (data.ContainsKey(word))
                    data[word].Remove(id);
            }
        }

        private void UpdateAllDocs(string word, int id)
        {
            if (allDocs.ContainsKey(id))
            {
                if (!allDocs[id].Contains(word))
                    allDocs[id].Add(word);
            }
            else
                allDocs.Add(id, new List<string> {word});
        }
        
        private void UpdateData(string word, int id, int position)
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