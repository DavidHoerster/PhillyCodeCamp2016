using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ActorWordCounter.Messages;
using ActorWordCounter.Writers;

namespace ActorWordCounter.Actors
{
    public class WordCountAggregatorActor : ReceiveActor
    {

        private Int32 _totalWords = 0;
        private Dictionary<String, Int32> _wordCounts;
        private IWriteStuff _writer;

        public static Props Create(IWriteStuff writer)
        {
            return Props.Create(() => new WordCountAggregatorActor(writer));
        }


        public WordCountAggregatorActor(IWriteStuff writer)
        {
            _wordCounts = new Dictionary<String, Int32>();
            _writer = writer;

            Receive<TotalWordCount>(msg =>
            {
                _totalWords = msg.TotalCount;
                DetermineIfCountsShouldBeDisplayed();
            });

            Receive<WordCount>(msg =>
            {
                _wordCounts.Add(msg.TheWord, msg.Count);
                DetermineIfCountsShouldBeDisplayed();
            });
        }

        private void DetermineIfCountsShouldBeDisplayed()
        {
            if (_totalWords > 0)
            {
                if (_wordCounts.Keys.Count == _totalWords)
                {
                    //all words are there...display the top 25 in order
                    var topWords = _wordCounts.OrderByDescending(w => w.Value)
                                    .Take(25);
                    foreach (var word in topWords)
                    {
                        _writer.WriteLine($"{word.Key} == {word.Value} times");
                    }
                }
            }
        }
    }
}
