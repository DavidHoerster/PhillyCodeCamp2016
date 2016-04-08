using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Domain.Helpers
{
    public class ActorMetaData
    {
        public ActorMetaData(string name, ActorMetaData parent = null)
        {
            Name = name;
            Parent = parent;
            // if no parent, we assume a top-level actor
            var parentPath = parent != null ? parent.Path : "/user";
            Path = string.Format("{0}/{1}", parentPath, Name);
        }

        public string Name { get; private set; }

        public ActorMetaData Parent { get; set; }

        public string Path { get; private set; }
    }
}
