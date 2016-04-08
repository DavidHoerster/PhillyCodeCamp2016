using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using RoundRobinWordCounter.Messages;
using RoundRobinWordCounter.Writers;

namespace RoundRobinWordCounter.Actors
{
    public class CountAggregator : ReceiveActor
    {
        public static Props Create(IWriteStuff writer)
        {
            return Props.Create(() => new CountAggregator(writer));
        }
        private Dictionary<String, Int32> _wordCounts;
        private IWriteStuff _writer;

        public CountAggregator(IWriteStuff writer)
        {
            _wordCounts = new Dictionary<String, Int32>();
            _writer = writer;

            Receive<CountWord>(msg =>
            {
                if (_wordCounts.ContainsKey(msg.Word))
                {
                    _wordCounts[msg.Word] += 1;
                }
                else
                {
                    _wordCounts.Add(msg.Word, 1);
                }
            });

            Receive<CountCompleted>(msg =>
            {
                //all words are there...display the top 25 in order
                var topWords = _wordCounts.OrderByDescending(w => w.Value)
                                .Take(25);
                foreach (var word in topWords)
                {
                    _writer.WriteLine($"{word.Key} == {word.Value} times");
                }
            });
        }
    }
}