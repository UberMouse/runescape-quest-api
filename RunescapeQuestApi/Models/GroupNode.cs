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
    }
}