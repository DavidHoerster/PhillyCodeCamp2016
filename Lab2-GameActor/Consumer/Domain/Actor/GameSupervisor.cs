using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Domain.Messages;

namespace Consumer.Domain.Actor
{
    public class GameSupervisor : ReceiveActor
    {
        public static Props Create()
        {
            return Props.Create<GameSupervisor>(() => new GameSupervisor());
        }

        public GameSupervisor()
        {
            Receive<HandleNewGameEvent>(msg =>
            {
                var gameId = $"game-{msg.GameId}";

                var gameActor = Context.Child(gameId);
                if (gameActor.IsNobody())
                {
                    var year = Convert.ToInt32(msg.GameId.Substring(3, 4));
                    var month = Convert.ToInt32(msg.GameId.Substring(7, 2));
                    var day = Convert.ToInt32(msg.GameId.Substring(9, 2));
                    var gameDate = new DateTime(year, month, day);
                    //create the new actor
                    gameActor = Context.ActorOf(GameActor.Create(msg.GameId, gameDate), gameId);
                }

                var batterSuper = Context.Child("batterSuper");
                if (batterSuper.IsNobody())
                {
                    batterSuper = Context.ActorOf(BatterSupervisor.Create(), "batterSuper");
                }
                var pitcherSuper = Context.Child("pitcherSuper");
                if (pitcherSuper.IsNobody())
                {
                    pitcherSuper = Context.ActorOf(PitcherSupervisor.Create(), "pitcherSuper");
                }

                gameActor.Tell(msg);
                batterSuper.Tell(msg);
                pitcherSuper.Tell(msg);
            });
        }
    }
}