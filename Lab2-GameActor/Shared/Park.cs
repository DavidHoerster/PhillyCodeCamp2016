using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Park
    {
        public Park(String id, String name, String city, String state,
            String league, DateTime first, DateTime? last)
        {
            Id = id; Name = name; City = city; State = state;
            League = league;
            First = first; Last = last;
        }

        public String Id { get; }
        public String Name { get; }
        public String City { get; }
        public String State { get; }
        public String League { get; }
        public DateTime First { get; }
        public DateTime? Last { get; }
    }
}
