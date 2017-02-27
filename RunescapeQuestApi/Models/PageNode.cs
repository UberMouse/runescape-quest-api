using System.Runtime.Serialization;

namespace RunescapeQuestApi.Models
{
    public class PageNode : BaseNode
    {
        public override NodeType Type => NodeType.Page;
        public override object Value => new
        {
            name = _name,
            link = _link
        };

        private readonly string _name;
        private readonly string _link;

        public PageNode(string name, string link)
        {
            _name = name;
            _link = link;
        }

        protected bool Equals(PageNode other)
        {
            return string.Equals(_name, other._name) && string.Equals(_link, other._link);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PageNode)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_name.GetHashCode() * 397) ^ _link.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"PageNode({nameof(_name)}: {_name}, {nameof(_link)}: {_link})";
        }
    }
}