using ActorTest.Actors;
using Akka.Actor;
using Akka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActorTest.Messages;

namespace ActorTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var system = ActorSystem.Create("timingTest");
            IActorRef super = system.ActorOf(Supervisor.Create(), "super");

            while (true)
            {
                Console.WriteLine("Actor Name: ");
                var name = Console.ReadLine();

                if (name.Equals("QUIT", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("BYE!");
                    return;
                }
                Console.WriteLine("Message: ");
                var msg = Console.ReadLine();

                super.Tell(new TellWorker(name, msg));
            }
        }
    }
}
