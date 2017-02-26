using System.Collections.Generic;
using System.IO;
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
        public void StartPointReturnsAListOfNodesForTheStartPoint()
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

        [Fact]
        public void MembersReturnsTrueWhenTheQuestIsMembersOnly()
        {
            var expected = true;
            var result = _subject.Members();

            Assert.Equal(
                expected,
                result
            );
        }

        [Fact]
        public void DifficultyReturnsAListOfNodesForDifficulty()
        {
            var expected = new List<IWikiNode>
            {
                new TextNode("Grandmaster")
            };
            var result = _subject.Difficulty();

            Assert.Equal(
                expected,
                result
            );
        }

        [Fact]
        public void LengthReturnsAListOfNodesForLength()
        {
            var expected = new List<IWikiNode>
            {
                new TextNode("Long (1-2 hours)")
            };
            var result = _subject.Length();

            Assert.Equal(
                expected,
                result
            );
        }
    }
}
