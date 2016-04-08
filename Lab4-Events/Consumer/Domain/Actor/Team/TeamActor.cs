using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Domain.Messages;

namespace Consumer.Domain.Actor
{
    public class TeamActor : ReceiveActor
    {
        public static Props Create(String id)
        {
            return Props.Create<TeamActor>(() => new TeamActor(id));
        }

        private readonly String _id;
        private Int32 _runs = 0;
        public TeamActor(String id)
        {
            _id = id;

            Receive<TeamRunScored>(msg =>
            {
                _runs += msg.Runs;
                Console.WriteLine($"{_id} has scored {_runs} runs");
            });
        }
    }
}
