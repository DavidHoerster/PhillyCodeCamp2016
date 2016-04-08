using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Domain.Messages;

namespace Consumer.Domain.Actor
{
    public class PitcherSupervisor : ReceiveActor
    {
        public static Props Create()
        {
            return Props.Create<PitcherSupervisor>(() => new PitcherSupervisor());
        }


        public PitcherSupervisor()
        {
            Receive<HandleNewGameEvent>(msg =>
            {
                var pitcherId = msg.PitcherId;
                var pitcherActor = Context.Child(pitcherId);
                if (pitcherActor.IsNobody())
                {
                    pitcherActor = Context.ActorOf(PitcherActor.Create(msg.PitcherId), pitcherId);
                }
                pitcherActor.Tell(new PitcherFacedBatter(msg.PitcherId,
                                                    "",
                                                    msg.HitValue,
                                                    msg.IsAtBat,
                                                    msg.IsSacrificeFly,
                                                    msg.PlayType));
            });

        }
    }
}
