using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Domain.Messages;

namespace Consumer.Domain.Actor
{
    public class TeamSupervisor : ReceiveActor
    {
        public static Props Create()
        {
            return Props.Create<TeamSupervisor>(() => new TeamSupervisor());
        }

        public TeamSupervisor()
        {
            Receive<TeamRunScored>(msg =>
            {
                var teamActor = Context.Child(msg.TeamId);
                if (teamActor.IsNobody())
                {
                    teamActor = Context.ActorOf(TeamActor.Create(msg.TeamId), msg.TeamId);
                }
                teamActor.Tell(msg);
            });
        }
    }
}
