using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace RunescapeQuestApi.Models
{
    public class QuestDetails
    {
        private enum Section { StartPoint, Members, Difficulty, Length, Requirements, Items, Enemies }
        private readonly IHtmlDocument _details;
        private readonly IHtmlDocument _rewards;
        private readonly WikiMarkupParser _parser;

        public QuestDetails(IHtmlDocument details, IHtmlDocument rewards)
        {
            _details = details;
            _rewards = rewards;
            _parser = new WikiMarkupParser();
        }

        public IEnumerable<BaseNode> StartPoint()
        {
            var startPoint = GetSection(Section.StartPoint);

            return _parser.Parse(startPoint);
        }

        public bool Members()
        {
            var members = GetSection(Section.Members);

            return members
                .Select(node => node.TextContent)
                .Any(text => text.Contains("only"));
        }

        private INodeList GetSection(Section section)
        {
            return _details.QuerySelectorAll("tr")[(int) section].QuerySelector("td").ChildNodes;
        }

        public IEnumerable<BaseNode> Difficulty()
        {
            var difficulty = GetSection(Section.Difficulty);

            return _parser.Parse(difficulty);
        }

        public IEnumerable<BaseNode> Length()
        {
            var length = GetSection(Section.Length);

            return _parser.Parse(length);
        }
    }
}