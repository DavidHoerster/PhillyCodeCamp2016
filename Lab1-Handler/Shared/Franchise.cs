using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Franchise
    {
        public Franchise(String id, String currentId, String league, String division,
            String location, String nickname, String city, String state,
            DateTime first, DateTime? last)
        {
            Id = id; CurrentId = currentId;
            League = league; Division = division; Location = location;
            NickName = nickname;
            City = city; State = state; First = first; Last = last;
        }

        public String CurrentId { get; }
        public String Id { get; }
        public String League { get; }
        public String Division { get; }
        public String Location { get; }
        public String NickName { get; }
        public String City { get; }
        public String State { get; }
        public DateTime First { get; }
        public DateTime? Last { get; }
    }
}
