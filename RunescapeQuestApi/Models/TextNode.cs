namespace RunescapeQuestApi.Models
{
    public class TextNode : IWikiNode
    {
        private readonly string _text;

        public TextNode(string text)
        {
            _text = text;
        }

        public NodeType Type()
        {
            return NodeType.Text;
        }

        public object Value()
        {
            return _text;
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