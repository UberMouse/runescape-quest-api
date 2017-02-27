using System.Linq;

namespace RunescapeQuestApi.Models
{
    public class GroupNode : IWikiNode
    {
        private readonly IWikiNode[] _nodes;

        public GroupNode(params IWikiNode[] nodes)
        {
            _nodes = nodes;
        }

        public NodeType Type()
        {
            return NodeType.Group;
        }

        public object Value()
        {
            return _nodes;
        }

        protected bool Equals(GroupNode other)
        {
            return _nodes.SequenceEqual(other._nodes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GroupNode)obj);
        }

        public override int GetHashCode()
        {
            return _nodes.GetHashCode();
        }
    }
}