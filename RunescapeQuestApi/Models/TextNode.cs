using System.Runtime.Serialization;

namespace RunescapeQuestApi.Models
{
    public class TextNode : BaseNode
    {
        public override NodeType Type => NodeType.Text;
        public override object Value => _text;

        private readonly string _text;

        public TextNode(string text)
        {
            _text = text;
        }

        protected bool Equals(TextNode other)
        {
            return string.Equals(_text, other._text);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TextNode)obj);
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }

        public override string ToString()
        {
            return $"TextNode({nameof(_text)}: {_text})";
        }    
    }
}