using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Domain.Messages;
using Shared;

namespace Consumer.Handler
{
    public class PlayHandler : IHandle<Play>
    {
        private readonly IActorRef _gameCoordinator;
        public PlayHandler(IActorRef gameCoordinator)
        {
            _gameCoordinator = gameCoordinator;
        }

        public void Handle(Play cmd)
        {
            var msg = new HandleNewGameEvent(cmd.Id, cmd.GameId, cmd.PlayType, cmd.HomeTeam, cmd.VisitingTeam,
                cmd.PitchSequence, cmd.Inning, cmd.Balls, cmd.Strikes, cmd.Outs, 
                cmd.HomeScore, cmd.VisitorScore, cmd.RbiOnPlay, cmd.HitValue, cmd.BatterId,
                cmd.PitcherId, cmd.IsBatterEvent, cmd.IsAtBat, cmd.IsHomeAtBat, cmd.IsEndGame, cmd.IsSacrificeFly);
            _gameCoordinator.Tell(msg);
        }
    }
}
