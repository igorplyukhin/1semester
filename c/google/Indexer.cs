using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Word
    {
        private Dictionary<int, List<int>> entries = new Dictionary<int, List<int>>();

        public Dictionary<int, List<int>> Entries
        {
            get => entries;
            set => entries = value;
        }
    }
    
    public class Indexer : IIndexer
    {
        private Dictionary<string, Word> data = new Dictionary<string, Word>();

        private HashSet<char> delimiters = new HashSet<char>() {' ', '.', ',', '!', '?', ':', '-', '\r', '\n'};

        public void Add(int id, string documentText)
        {
            var buffer = "";
            for (var i = 0; i < documentText.Length; i++)
            {
                var ch = documentText[i];
                if (delimiters.Contains(ch))
                {
                    data.Add(buffer,new Word());
                    data[buffer].Entries.Add(id, i - buffer.Length);
                    buffer = "";
                }
                else
                {
                    buffer += ch;
                }
            }
        }

        public List<int> GetIds(string word)
        {
            return new List<int>();
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