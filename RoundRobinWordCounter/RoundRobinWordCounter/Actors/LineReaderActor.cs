using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Akka.Actor;
using RoundRobinWordCounter.Messages;
using RoundRobinWordCounter.Writers;

namespace RoundRobinWordCounter.Actors
{
    public class LineReaderActor : ReceiveActor
    {
        public static Props Create(IWriteStuff writer, IActorRef aggregator)
        {
            return Props.Create(() => new LineReaderActor(writer, aggregator));
        }

        private readonly IWriteStuff _writer;
        private IActorRef _aggregator;
        public LineReaderActor(IWriteStuff writer, IActorRef aggregator)
        {
            _writer = writer;
            _aggregator = aggregator;

            Receive<ReadLineForCounting>(msg =>
            {
                var cleanFileContents = Regex.Replace(msg.Line, @"[^\u0000-\u007F]", " ");

                var wordArray = cleanFileContents.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in wordArray)
                {
                    aggregator.Tell(new CountWord(word, Self.Path.Name));
                }
            });

            Receive<CountCompleted>(msg =>
            {
                Sender.Tell(msg);
            });
        }
    }
}
