using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Consumer.Domain.Messages
{
    public class HitterWasAtBat
    {
        public HitterWasAtBat(String id, String name, String team, String pitcherId, Int32 hitValue, Int32 rbiOnPlay,
            Int32 balls, Int32 strikes, Int32 outs, Int32 inning, 
            Boolean isAtBat, Boolean isSacrificeFly, PlayType playType)
        {
            Id = id;
            Name = name;
            HitValue = hitValue;
            RbiOnPlay = rbiOnPlay;
            IsAtBat = isAtBat;
            IsSacrificeFly = isSacrificeFly;
            PlayType = playType;
            Team = team;
            PitcherId = pitcherId;
            Balls = balls;
            Strikes = strikes;
            Outs = outs;
            Inning = inning;
        }

        public readonly String Id;
        public readonly String Name;
        public readonly String PitcherId;
        public readonly Int32 HitValue;
        public readonly Int32 RbiOnPlay;
        public readonly Int32 Balls;
        public readonly Int32 Strikes;
        public readonly Int32 Outs;
        public readonly Int32 Inning;
        public readonly Boolean IsAtBat;
        public readonly PlayType PlayType;
        public readonly Boolean IsSacrificeFly;
        public readonly String Team;
    }
}