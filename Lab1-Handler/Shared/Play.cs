using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Play
    {
        public Play(String gameId, PlayType playType, String home, String visitor,
            String pitchSequence, Int32 balls, Int32 strikes, Int32 outs, String batterId, String pitcherId,
            Boolean isBatterEvent, Boolean isAtBat)
        {
            GameId = gameId;
            PlayType = playType;
            HomeTeam = home;
            VisitingTeam = visitor;
            PitchSequence = pitchSequence;
            Balls = balls;
            Strikes = strikes;
            Outs = outs;
            BatterId = batterId;
            PitcherId = pitcherId;
            IsBatterEvent = isBatterEvent;
            IsAtBat = isAtBat;
        }


        public Guid Id => Guid.NewGuid();
        public String GameId { get; }
        public PlayType PlayType { get; }
        public String HomeTeam { get; }
        public String VisitingTeam { get; }
        public String PitchSequence { get; }
        public Int32 Balls { get; }
        public Int32 Strikes { get; }
        public Int32 Outs { get; }
        public String BatterId { get; }
        public String PitcherId { get; }
        public Boolean IsBatterEvent { get; }
        public Boolean IsAtBat { get; }

    }
}
