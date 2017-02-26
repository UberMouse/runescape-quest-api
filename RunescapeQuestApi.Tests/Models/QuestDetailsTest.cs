using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Parser.Html;
using RunescapeQuestApi.Models;
using Xunit;

namespace RunescapeQuestApi.Tests.Models
{
    public class QuestDetailsTest
    {
        private readonly QuestDetails _subject;

        public QuestDetailsTest()
        {
            var parser = new HtmlParser();

            var details = parser.Parse(File.ReadAllText("Models/quest_details.html"));
            var rewards = parser.Parse(File.ReadAllText("Models/quest_rewards.html"));

            _subject = new QuestDetails(details, rewards);
        }

        [Fact]
        public void QuestDetailsStartPointWorks()
        {
            var expected = new List<IWikiNode>
            {
                new TextNode("Speak to"),
                new PageNode("King Veldaban", "/wiki/King_Veldaban"),
                new TextNode("on the top floor of"),
                new PageNode("Keldagrim Palace", "/wiki/Keldagrim_Palace")
            };
            var result = _subject.StartPoint();

            Assert.Equal(
                expected,
                result
            );
        }
    }
}
