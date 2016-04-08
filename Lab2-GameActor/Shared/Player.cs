using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Player
    {
        public Player(String id, String first, String last, DateTime debut)
        {
            Id = id; First = first; Last = last; Debut = debut;
        }
        public String Id { get; }
        public String First { get; }
        public String Last { get; }
        public DateTime Debut { get; }
        public String FullName { get { return $"{First} {Last}"; } }
    }
}
