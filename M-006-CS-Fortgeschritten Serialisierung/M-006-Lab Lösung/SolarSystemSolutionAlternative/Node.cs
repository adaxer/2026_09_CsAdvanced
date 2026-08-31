using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SolarSystemSolutionAlternative
{
    public class Node<T>
    {
        private Guid _id;
        
        private T _item;

        private Guid _parentId =  default!;

        private List<Guid> _children = new List<Guid>();

        public Node()
        {

        }
        public Node(T item)
        {
            Item = item;
            Id = Guid.NewGuid();
        }

        public Node(T item, Guid parentId)
            :this(item)
        {
            ParentId = parentId;
        }

        public Node(Node<T> node)
        {
            this.Id = node.Id;
            this.ParentId = node.ParentId;
            this.Item = node.Item;
            this.Children = node.Children;
        }


        [JsonPropertyName("Id")]
        public Guid Id { get => _id; set => _id = value; }

        [JsonPropertyName("Item")]
        public T Item { get => _item; set => _item = value; }

        [JsonPropertyName("ParentId")]
        public Guid ParentId { get => _parentId; set => _parentId = value; }

        [JsonPropertyName("Children")]
        public List<Guid> Children { get => _children; set => _children = value; }

        public void AddChild (Node<T> childNode)
        {
            childNode.ParentId = Id;
            Children.Add(childNode.Id);
        }
    }
}
