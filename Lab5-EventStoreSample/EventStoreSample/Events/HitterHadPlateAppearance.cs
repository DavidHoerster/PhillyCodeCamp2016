using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class HitterHadPlateAppearance : EventBase
    {
        public String PlayerId { get; set; }
        public String PitcherId { get; set; }
        public Int32 Balls { get; set; }
        public Int32 Strikes { get; set; }
        public Int32 Outs { get; set; }
        public Int32 RbiOnPlay { get; set; }
        public Int32 HitValue { get; set; }
        public Int32 Inning { get; set; }
        public Boolean IsAtBat { get; set; }
        public PlayType PlayType { get; set; }
        public String Team { get; set; }
    }
}
