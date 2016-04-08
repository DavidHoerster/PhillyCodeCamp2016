using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace HelloAkka
{
    public class SayMyNameActor : ReceiveActor
    {

        public static Props Create()
        {
            return Props.Create(() => new SayMyNameActor());
        }

        public SayMyNameActor()
        {
            Receive<SayMyName>(msg =>
            {
                //Console.WriteLine($"Hi there, {msg.Name}");

                #region A little more complicated
                if (msg.Name.Equals("Heisenberg"))
                {
                    Context.ActorOf(CowerInFearActor.Create()).Tell("Yo, Mr. White!");
                }
                else
                {
                    Console.WriteLine($"Hi there, {msg.Name}");
                }
                #endregion
            });
        }
    }
}
