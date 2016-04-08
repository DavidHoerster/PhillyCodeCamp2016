using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace HelloAkka
{
    class Program
    {
        static void Main(string[] args)
        {

            var system = ActorSystem.Create("myName");

            var name = system.ActorOf(SayMyNameActor.Create(), "announcer");
            // actor path ==>  akka://myName/user/announcer

            name.Tell(new SayMyName("Heisenberg"));
            name.Tell(new SayMyName("Jesse"));
            name.Tell(new SayMyName("Saul"));
            name.Tell(new SayMyName("Heisenberg"));
            name.Tell(new SayMyName("Jesse"));
            name.Tell(new SayMyName("Saul"));
            name.Tell(new SayMyName("Heisenberg"));
            name.Tell(new SayMyName("Jesse"));
            name.Tell(new SayMyName("Saul"));

            Console.ReadLine();
        }
    }
}
