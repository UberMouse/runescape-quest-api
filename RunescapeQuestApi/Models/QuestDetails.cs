using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngleSharp.Dom.Html;

namespace RunescapeQuestApi.Models
{
    public class QuestDetails
    {
        private readonly IHtmlDocument _details;
        private readonly IHtmlDocument _rewards;
        private readonly WikiMarkupParser _parser;

        public QuestDetails(IHtmlDocument details, IHtmlDocument rewards)
        {
            _details = details;
            _rewards = rewards;
            _parser = new WikiMarkupParser();
        }

        public IEnumerable<IWikiNode> StartPoint()
        {
            var startPoint = _details.QuerySelectorAll("tr")[0].QuerySelector("td").ChildNodes;

            return _parser.Parse(startPoint);
        }
    }
}