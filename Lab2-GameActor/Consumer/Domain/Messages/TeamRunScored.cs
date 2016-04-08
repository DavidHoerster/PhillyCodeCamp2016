using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Domain.Messages
{
    public class TeamRunScored
    {
        public readonly String TeamId;
        public readonly Int32 Runs;

        public TeamRunScored(String teamId, Int32 runs)
        {
            TeamId = teamId; Runs = runs;
        }
    }
}
