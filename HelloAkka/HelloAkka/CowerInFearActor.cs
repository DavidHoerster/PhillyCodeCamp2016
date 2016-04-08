using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace HelloAkka
{
    public class CowerInFearActor : ReceiveActor
    {

        public static Props Create()
        {
            return Props.Create(() => new CowerInFearActor());
        }

        public CowerInFearActor()
        {
            Receive<String>(msg =>
            {
                Console.WriteLine("Aren't you Walter White?");
            });
        }
    }
}