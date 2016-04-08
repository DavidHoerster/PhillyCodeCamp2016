using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Play
    {
        public Play(String gameId, PlayType playType, String homeTeam, String visitingTeam,
            String pitchSequence, Int32 inning, Int32 balls, Int32 strikes, Int32 outs, 
            Int32 homeScore, Int32 visitorScore, Int32 rbiOnPlay, Int32 hitValue,
            String batterId, String pitcherId,
            Boolean isBatterEvent, Boolean isAtBat, Boolean isHomeAtBat, Boolean isEndGame,
            Boolean isSacrificeFly)
        {
            GameId = gameId;
            PlayType = playType;
            HomeTeam = homeTeam;
            VisitingTeam = visitingTeam;
            PitchSequence = pitchSequence;
            Balls = balls;
            Strikes = strikes;
            Outs = outs;
            Inning = inning;
            HomeScore = homeScore;
            VisitorScore = visitorScore;
            RbiOnPlay = rbiOnPlay;
            HitValue = hitValue;
            BatterId = batterId;
            PitcherId = pitcherId;
            IsBatterEvent = isBatterEvent;
            IsAtBat = isAtBat;
            IsHomeAtBat = isHomeAtBat;
            IsEndGame = isEndGame;
            IsSacrificeFly = isSacrificeFly;
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
        public Int32 Inning { get; }
        public Int32 HomeScore { get; }
        public Int32 VisitorScore { get; }
        public Int32 RbiOnPlay { get; }
        public Int32 HitValue { get; }
        public String BatterId { get; }
        public String PitcherId { get; }
        public Boolean IsBatterEvent { get; }
        public Boolean IsAtBat { get; }
        public Boolean IsHomeAtBat { get; }
        public Boolean IsEndGame { get; }
        public Boolean IsSacrificeFly { get; }
    }
}
