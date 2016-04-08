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
        public HitterWasAtBat(String id, String name, Int32 hitValue, Int32 rbiOnPlay, Boolean isAtBat, Boolean isSacrificeFly, PlayType playType)
        {
            Id = id;
            Name = name;
            HitValue = hitValue;
            RbiOnPlay = rbiOnPlay;
            IsAtBat = isAtBat;
            IsSacrificeFly = isSacrificeFly;
            PlayType = playType;
        }

        public readonly String Id;
        public readonly String Name;
        public readonly Int32 HitValue;
        public readonly Int32 RbiOnPlay;
        public readonly Boolean IsAtBat;
        public readonly PlayType PlayType;
        public readonly Boolean IsSacrificeFly;

    }
}