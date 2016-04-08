using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorTest.Messages
{
    public class TellWorker
    {
        public readonly String Message, Name;

        public TellWorker(String name, String msg) { Name = name; Message = msg; }
    }
}
