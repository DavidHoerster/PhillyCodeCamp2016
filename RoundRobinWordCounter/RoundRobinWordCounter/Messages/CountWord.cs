using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobinWordCounter.Messages
{
    public class CountWord
    {
        public readonly String Word;
        public readonly String Path;
        public CountWord(String word, String path) { Word = word; Path = path; }
    }
}
