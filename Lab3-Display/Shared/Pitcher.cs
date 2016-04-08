using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Pitcher
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Hits { get; set; }
        public Int32 Walks { get; set; }
        public Int32 AtBats { get; set; }
        public Int32 TotalBases { get; set; }
        public Int32 HitByPitch { get; set; }
        public Double OppAvg { get; set; }
        public Double OppObp { get; set; }
        public Double OppSlugging { get; set; }
        public Int32 SacrificeFlies { get; set; }
    }
}
