using System;
using Akka.Actor;
using Consumer.Domain.Helpers;
using Consumer.Domain.Messages;

namespace Consumer.Domain.Actor
{
    public class GameActor : ReceiveActor
    {
        public static Props Create(String id, DateTime gameDate)
        {
            return Props.Create<GameActor>(() => new GameActor(id, gameDate));
        }

        private readonly String _id;
        private readonly DateTime _gameDate;
        private String _homeTeam, _visitingTeam;
        private Int32 _inning, _outs, _balls, _strikes, _homeScore, _visitorScore;
        private String _batterId, _pitcherId;

        public GameActor(String id, DateTime gameDate)
        {
            _id = id;
            _gameDate = gameDate;
            Receive<HandleNewGameEvent>(msg =>
            {
                _homeTeam = msg.HomeTeam;
                _visitingTeam = msg.VisitingTeam;
                _inning = msg.Inning;
                _outs = msg.Outs;
                _strikes = msg.Strikes;
                _balls = msg.Balls;
                _homeScore = msg.HomeScore;
                _visitorScore = msg.VisitorScore;
                _batterId = msg.BatterId;
                _pitcherId = msg.PitcherId;

                Console.WriteLine($"{_gameDate} :: {_homeTeam} vs {_visitingTeam}: {_homeScore} - {_visitorScore} :: {_inning} {_outs} {_strikes} {_balls}");

                if (msg.RbiOnPlay > 0)
                {
                    var runScoredMsg = new TeamRunScored(msg.IsHomeAtBat ? _homeTeam : _visitingTeam, msg.RbiOnPlay);
                    Context.ActorSelection(ActorPaths.GameEventCoordinator.Path).Tell(runScoredMsg);
                }
            });
        }
    }
}