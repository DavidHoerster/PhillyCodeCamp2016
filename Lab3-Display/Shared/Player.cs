using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Player
    {
        public Player(String id, String first, String last, String debut)
        {
            Id = id; First = first; Last = last; Debut = debut;
        }
        public String Id { get; private set; }
        public String First { get; private set; }
        public String Last { get; private set; }
        public String Debut { get; private set; }
        public String FullName { get { return $"{First} {Last}"; } }
    }
}
