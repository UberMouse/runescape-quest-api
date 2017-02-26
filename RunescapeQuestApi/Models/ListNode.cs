using System.Collections.Generic;

namespace RunescapeQuestApi.Models
{
    public class ListNode : IWikiNode
    {
        private readonly List<IWikiNode> _children;

        public ListNode(List<IWikiNode> children)
        {
            _children = children;
        }

        public NodeType Type()
        {
            return NodeType.List;
        }

        public object Value()
        {
            return _children;
        }

        protected bool Equals(ListNode other)
        {
            return _children.Equals(other._children);
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