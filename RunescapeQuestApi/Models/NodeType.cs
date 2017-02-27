using System;

namespace RunescapeQuestApi.Models
{
    public class NodeType
    {
        public static NodeType Text => new NodeType("Text");
        public static NodeType Page => new NodeType("Page");
        public static NodeType Group => new NodeType("Group");
        public static NodeType List => new NodeType("List");

        private readonly string _type;

        public NodeType(string type)
        {
            _type = type;
        }

        protected bool Equals(NodeType other)
        {
            return string.Equals(_type, other._type);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NodeType) obj);
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode();
        }

        public override string ToString()
        {
            return _type;
        }
    }
}