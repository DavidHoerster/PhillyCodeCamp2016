using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public abstract class EventBase
    {
        public Guid Id { get; set; }
        public Int32 Version { get; set; }
    }
}
