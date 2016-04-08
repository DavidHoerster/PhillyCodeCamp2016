using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Entity
{
    public class Batter
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Hits { get; set; }
        public Int32 Walks { get; set; }
        public Int32 AtBats { get; set; }
        public Int32 TotalBases { get; set; }
        public Int32 HitByPitch { get; set; }
        public Double Average { get; set; }
        public Double OnBase { get; set; }
        public Double Slugging { get; set; }
        public Int32 SacrificeFlies { get; set; }
    }
}
