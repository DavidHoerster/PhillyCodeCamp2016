using ActorTest.Messages;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorTest.Actors
{
    public class Worker : ReceiveActor
    {
        private readonly String _name;
        private IEnumerable<String> _messages;
        private Int32 _messageCount;
        public static Props Create(String name)
        {
            return Props.Create<Worker>(name);
        }

        public Worker(String name)
        {
            _name = name;
            _messageCount = 0;
            _messages = new List<String>();

            Receive<TellWorker>(msg =>
            {
                _messageCount += 1;
                _messages = _messages.Concat(new String[] { msg.Message });
                Console.WriteLine($"Message {_messageCount}: {msg.Message}");
                //Console.WriteLine("Messages in the List:");
                //foreach (var m in _messages)
                //{
                //    Console.WriteLine(m);
                //}
            });
        }

        protected override void PostStop()
        {
            Console.WriteLine($"{_name} stopped after receiving {_messageCount} messages");
            base.PostStop();
        }
    }
}
