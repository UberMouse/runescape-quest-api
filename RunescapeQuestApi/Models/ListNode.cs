using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RunescapeQuestApi.Models
{
    public class ListNode : BaseNode
    {
        public override NodeType Type => NodeType.List;
        public override object Value => _children;

        private readonly List<BaseNode> _children;

        public ListNode(List<BaseNode> children)
        {
            _children = children;
        }

        protected bool Equals(ListNode other)
        {
            return _children.SequenceEqual(other._children);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ListNode)obj);
        }

        public override int GetHashCode()
        {
            return _children.GetHashCode();
        }

        public override string ToString()
        {
            return $"ListNode(child count: {_children.Count})";
        }
    }
}