using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ActorWordCounter.Messages;
using ActorWordCounter.Writers;

namespace ActorWordCounter.Actors
{
    public class CountSupervisor : ReceiveActor
    {
        public static Props Create(IWriteStuff writer)
        {
            return Props.Create(() => new CountSupervisor(writer));
        }

        private readonly IWriteStuff _writer;
        public CountSupervisor(IWriteStuff writer)
        {
            _writer = writer;
            Receive<StartCount>(msg =>
            {
                var fileInfo = new FileInfo(msg.FileName);
                var lineReader = Context.ActorOf(LineReaderActor.Create(writer), fileInfo.Name);

                using (var reader = fileInfo.OpenText())
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineReader.Tell(new ReadLineForCounting(line));
                    }
                }

                lineReader.Tell(new Complete());
            });
        }
    }
}
