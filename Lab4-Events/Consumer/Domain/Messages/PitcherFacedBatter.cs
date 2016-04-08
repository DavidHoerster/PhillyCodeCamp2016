using System;
using Shared;

namespace Consumer.Domain.Messages
{
    public class PitcherFacedBatter
    {
        public PitcherFacedBatter(String id, String name, Int32 hitValue, Boolean isAtBat, Boolean isSacrificeFly, PlayType playType)
        {
            Id = id;
            Name = name;
            HitValue = hitValue;
            IsAtBat = isAtBat;
            IsSacrificeFly = isSacrificeFly;
            PlayType = playType;
        }

        public readonly String Id;
        public readonly String Name;
        public readonly Int32 HitValue;
        public readonly Boolean IsAtBat;
        public readonly PlayType PlayType;
        public readonly Boolean IsSacrificeFly;
    }
}