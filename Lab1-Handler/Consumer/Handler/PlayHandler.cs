using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Consumer.Handler
{
    public class PlayHandler : IHandle<Play>
    {
        public void Handle(Play cmd)
        {
            Console.WriteLine($"Batter {cmd.BatterId} against Pitcher {cmd.PitcherId} with pitch sequence {cmd.PitchSequence} was a {cmd.PlayType}.");
        }
    }
}
