using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Domain.Messages;

namespace Consumer.Domain.Actor
{
    public class GameEventCoordinator : ReceiveActor
    {

        public static Props Create()
        {
            return Props.Create<GameEventCoordinator>(() => new GameEventCoordinator());
        }

        public GameEventCoordinator()
        {
            Receive<HandleNewGameEvent>(cmd =>
            {
                var gameSuper = Context.Child("gameSuper");
                if (gameSuper.IsNobody())
                {
                    gameSuper = Context.ActorOf(GameSupervisor.Create(), "gameSuper");
                }
                gameSuper.Tell(cmd);
                //Console.WriteLine($"Batter {cmd.BatterId} against Pitcher {cmd.PitcherId} with pitch sequence {cmd.PitchSequence} was a {cmd.PlayType}.");
            });

            Receive<TeamRunScored>(msg =>
            {
                var teamSuper = Context.Child("teamSuper");
                if (teamSuper.IsNobody())
                {
                    teamSuper = Context.ActorOf(TeamSupervisor.Create(), "teamSuper");
                }
                teamSuper.Tell(msg);
            });
        }
    }
}
