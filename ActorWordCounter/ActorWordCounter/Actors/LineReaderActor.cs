using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Akka.Actor;
using ActorWordCounter.Messages;
using ActorWordCounter.Writers;

namespace ActorWordCounter.Actors
{
    public class LineReaderActor  : ReceiveActor
    {
        public static Props Create(IWriteStuff writer)
        {
            return Props.Create(() => new LineReaderActor(writer));
        }

        private readonly IWriteStuff _writer;
        private IActorRef _wordAggregator;

        public LineReaderActor(IWriteStuff writer)
        {
            _writer = writer;
            _wordAggregator = Context.ActorOf(WordCountAggregatorActor.Create(writer), "aggregator");

            Receive<ReadLineForCounting>(msg =>
            {
                var cleanFileContents = Regex.Replace(msg.Line, @"[^\u0000-\u007F]", " ");

                var wordArray = cleanFileContents.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in wordArray)
                {
                    var wordCounter = Context.Child(word);
                    if (wordCounter.IsNobody())
                    {
                        wordCounter = Context.ActorOf(WordCounterActor.Create(_writer, _wordAggregator, word), word);
                    }

                    wordCounter.Tell(new CountWord());
                }
            });

            Receive<Complete>(msg =>
            {
                var childCount = -1;    //TODO: doesn't work with 0
                var children = Context.GetChildren();

                foreach (var child in children)
                {
                    child.Tell(new DisplayWordCount());
                    childCount++;
                }
                _wordAggregator.Tell(new TotalWordCount(childCount));
            });
        }
    }
}
