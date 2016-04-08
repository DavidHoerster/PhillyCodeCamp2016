using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Data;
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

                    //get hitter events for this batter from event store
                    var start = 0; var increment = 100;
                    var endOfStream = false;
                    do
                    {
                        var eventSlice = EventConnection.Connection.ReadStreamEventsForwardAsync(msg.BatterId, start, start + increment, false).Result;
                        start += increment + 1;
                        var events = eventSlice.Events.Select(e => Newtonsoft.Json.JsonConvert.DeserializeObject<HitterWasAtBat>(System.Text.Encoding.Default.GetString(e.Event.Data)));
                        batterActor.Tell(events);
                        endOfStream = eventSlice.IsEndOfStream;
                    } while (!endOfStream);
                }
                batterActor.Tell(new HitterWasAtBat(msg.BatterId,
                                                    "",
                                                    msg.IsHomeAtBat ? msg.HomeTeam : msg.VisitingTeam,
                                                    msg.PitcherId,
                                                    msg.HitValue,
                                                    msg.RbiOnPlay,
                                                    msg.Balls,
                                                    msg.Strikes,
                                                    msg.Outs,
                                                    msg.Inning,
                                                    msg.IsAtBat,
                                                    msg.IsSacrificeFly,
                                                    msg.PlayType));
            });
        }
    }
}
