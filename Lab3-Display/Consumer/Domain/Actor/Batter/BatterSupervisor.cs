using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Domain.Messages;

namespace Consumer.Domain.Actor
{
    public class BatterSupervisor : ReceiveActor
    {
        public static Props Create()
        {
            return Props.Create<BatterSupervisor>(() => new BatterSupervisor());
        }


        public BatterSupervisor()
        {
            Receive<HandleNewGameEvent>(msg =>
            {
                var batterId = msg.BatterId;
                var batterActor = Context.Child(batterId);
                if (batterActor.IsNobody())
                {
                    batterActor = Context.ActorOf(BatterActor.Create(msg.BatterId), batterId);
                }
                batterActor.Tell(new HitterWasAtBat(msg.BatterId,
                                                    "",
                                                    msg.HitValue,
                                                    msg.RbiOnPlay,
                                                    msg.IsAtBat,
                                                    msg.IsSacrificeFly,
                                                    msg.PlayType));
            });
        }
    }
}
