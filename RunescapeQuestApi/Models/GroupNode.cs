using System.Linq;

namespace RunescapeQuestApi.Models
{
    public class GroupNode : BaseNode
    {
        public override NodeType Type => NodeType.Group;
        public override object Value => _nodes;

        private readonly BaseNode[] _nodes;

        public GroupNode(params BaseNode[] nodes)
        {
            _nodes = nodes;
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