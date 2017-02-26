using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using RunescapeQuestApi.Models;
using Xunit;
using INode = RunescapeQuestApi.Models.IWikiNode;

namespace RunescapeQuestApi.Tests.Models
{
    public class WikiMarkupParserTest
    {
        private readonly INodeList _startTdNodes;

        public WikiMarkupParserTest()
        {
            var parser = new HtmlParser();
            var details = parser.Parse(File.ReadAllText("Models/quest_details.html"));

            // For some reason if I extract just this fragment and inline it the parser is unhappy
            _startTdNodes = details.QuerySelectorAll(".questdetails-info")[0].ChildNodes;
        }

        [Fact]
        public void CanParseBasicMarkup()
        {
            var expected = new List<INode>
            {
                new TextNode("Speak to"),
                new PageNode("King Veldaban", "/wiki/King_Veldaban"),
                new TextNode("on the top floor of"),
                new PageNode("Keldagrim Palace", "/wiki/Keldagrim_Palace")
            };
            var result = new WikiMarkupParser().Parse(_startTdNodes);

            Assert.Equal(expected, result);
        }
    }
}
