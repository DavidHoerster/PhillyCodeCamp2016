using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;
using RoundRobinWordCounter.Messages;
using RoundRobinWordCounter.Writers;

namespace RoundRobinWordCounter.Actors
{
    public class CountSupervisor : ReceiveActor
    {
        public static Props Create(IWriteStuff writer)
        {
            return Props.Create(() => new CountSupervisor(writer));
        }
        private readonly IWriteStuff _writer;
        private readonly Int32 _numberOfRoutees;
        private Int32 _completeReaders;

        public CountSupervisor(IWriteStuff writer)
        {
            _writer = writer;
            _numberOfRoutees = 5;
            _completeReaders = 0;
            
            var _aggregator = Context.ActorOf(CountAggregator.Create(_writer), "aggregator");

            Receive<StartCount>(msg =>
            {
                var fileInfo = new FileInfo(msg.FileName);
                var lineReader = Context.ActorOf(new RoundRobinPool(_numberOfRoutees).Props(LineReaderActor.Create(writer, _aggregator)));

                using (var reader = fileInfo.OpenText())
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineReader.Tell(new ReadLineForCounting(line));
                    }
                }

                lineReader.Tell(new Broadcast(new CountCompleted()));
            });

            Receive<CountCompleted>(msg =>
            {
                _completeReaders += 1;
                if (_completeReaders == _numberOfRoutees)
                {
                    _aggregator.Tell(msg);
                }
            });
        }
    }
}
