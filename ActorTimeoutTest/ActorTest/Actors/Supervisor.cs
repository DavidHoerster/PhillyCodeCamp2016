using ActorTest.Messages;
using Akka.Actor;
using System;
using System.Collections.Generic;

namespace ActorTest.Actors
{
    public class Supervisor : ReceiveActor
    {
        private const int ACTOR_TIMEOUT = (1000 * 15);      //SECONDS
        private IDictionary<String, ICancelable> _cancelableActors;
        public static Props Create()
        {
            return Props.Create<Supervisor>();
        }

        public Supervisor()
        {
            _cancelableActors = new Dictionary<String, ICancelable>();
            Receive<TellWorker>(msg =>
            {
                var child = Context.Child(msg.Name);
                if (child == ActorRefs.Nobody)
                {
                    child = Context.ActorOf(Worker.Create(msg.Name), msg.Name);
                }
                CreateCancelCallbackForActor(msg, child);

                child.Tell(msg);
            });
        }

        private void CreateCancelCallbackForActor(TellWorker msg, IActorRef child)
        {
            var token = Context.System.Scheduler.ScheduleTellOnceCancelable(ACTOR_TIMEOUT, child, PoisonPill.Instance, Self);
            if (_cancelableActors.ContainsKey(msg.Name))
            {
                _cancelableActors[msg.Name].Cancel();
                _cancelableActors[msg.Name] = token;
            }
            else
            {
                _cancelableActors.Add(msg.Name, token);
            }
        }
    }
}