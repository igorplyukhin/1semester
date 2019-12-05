using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private Dictionary<int, List<string>> data = new Dictionary<int, List<string>>();

        private char[] delimiters = {' ', '.', ',', '!', '?', ':', '-', '\r', '\n'};

        public void Add(int id, string documentText)
        {
            data[id] = documentText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public List<int> GetIds(string word)
        {
            return data.Keys.Where(id => data[id].Contains(word)).ToList();
        }

        public List<int> GetPositions(int id, string word)
        {
            return new List<int>();
        }

        public void Remove(int id)
        {
            
        }
    }
}