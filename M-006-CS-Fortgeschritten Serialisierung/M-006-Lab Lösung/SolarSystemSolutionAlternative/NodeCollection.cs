using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystemSolutionAlternative
{
    public class NodeCollection<T> : List<Node<T>>
    {
        public NodeCollection()
        {

        }
        public Node<T>? GetRootNode()
            => this.SingleOrDefault(x => x.ParentId == Guid.Empty);
        public Node<T>? GetNode(Guid id)
            => this.SingleOrDefault(x => x.Id == id);

        public Guid FindParentGuid(Guid id)
           => this.SingleOrDefault(x => x.ParentId == id).ParentId;

        public List<Guid> Childs(Guid id)
            => GetNode(id).Children;
    }
}
