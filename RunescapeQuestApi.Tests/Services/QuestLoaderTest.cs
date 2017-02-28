using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunescapeQuestApi.Models;
using Xunit;

namespace RunescapeQuestApi.Tests.Services
{
    public class QuestLoaderTest : IClassFixture<QuestFixture>
    {
        internal Quest _quest = new Quest()
        {
            Difficulty = new List<BaseNode>() { new GroupNode(new TextNode("Intermediate")) },
            Length = new List<BaseNode>() { new GroupNode(new TextNode("Medium")) },
            QuestRequirements = new List<BaseNode>()
                {
                    new ListNode(new List<BaseNode>()
                    {
                        new PageNode("Death to the Dorgeshuun", "http://www.runehq.com/guide.php?type=quest&id=0734"),
                        new PageNode("Giant Dwarf, The", "http://www.runehq.com/guide.php?type=quest&id=0512"),
                        new PageNode("Dig Site, The", "http://www.runehq.com/guide.php?type=quest&id=0354")
                    })
                },
            SkillRequirements = new List<BaseNode>()
                {
                    new ListNode(new List<BaseNode>()
                    {
                        new GroupNode(
                            new TextNode("15"),
                            new PageNode("Attack", "http://www.runehq.com/guide.php?type=skill&id=561#attack")
                        ),
                        new GroupNode(
                            new TextNode("25"),
                            new PageNode("Prayer", "http://www.runehq.com/guide.php?type=skill&id=295")
                        )
                    })
                },
            ItemsNeededAtStart = new List<BaseNode>()
                {
                    new ListNode(new List<BaseNode>()
                    {
                        new TextNode("Light source (preferably a lantern)"),
                        new TextNode("Magic or Ranged attack method"),
                        new TextNode("Armor"),
                        new TextNode("A Weapon.")
                    })
                },
            ItemsNeededToComplete = new List<BaseNode>()
                {
                    new ListNode(new List<BaseNode>()
                    {
                        new TextNode("None.")
                    })
                },
            ItemsRecommended = new List<BaseNode>()
                {
                    new ListNode(new List<BaseNode>()
                    {                       
                        new TextNode("Dorgesh-kaan sphere.")
                    })
                },
            QuestPoints = new List<BaseNode>()
                {
                    new TextNode("1")
                },
            Reward = new List<BaseNode>()
                {
                    new ListNode(new List<BaseNode>()
                    {
                         new TextNode("3K Mining XP"),
                         new TextNode("3K Prayer XP"),
                         new TextNode("Ancient mace"),
                         new TextNode("Access to the Dorgesh-Kaan-Keldagrim train service"),
                         new TextNode("The ability to buy Goblin Village spheres from Oldak"),
                         new TextNode("2 Treasure Hunter keys.")
                    })
                },
            StartPoint = new List<BaseNode>()
                {
                    new GroupNode(
                        new TextNode("Ur-Tag's house in"),
                        new PageNode("Dorgesh-Kaan", "http://www.runehq.com/guide.php?type=city&id=917"),
                        new TextNode(".")
                    )
                },
            ToStart = new List<BaseNode>()
                {
                    new GroupNode(
                        new TextNode("Speak to either"),
                        new PageNode("Ur-Tag", "http://www.runehq.com/database.php?type=person&id=1254"),
                        new TextNode("or"),
                        new PageNode("Ambassador Alvijar", "http://www.runehq.com/database.php?type=person&id=1253"),
                        new TextNode(".")
                    )
                }
        };
        internal QuestFixture Fixture { get; set; }

        public QuestLoaderTest(QuestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task QuestDifficultyIsParsed()
        {
            var expected = _quest.Difficulty;
            var result = (await Fixture.LoadQuest()).Difficulty;

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task QuestLengthIsParsed()
        {
            var expected = _quest.Length;
            var result = (await Fixture.LoadQuest()).Length;

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task QuestRequirementsAreParsed()
        {
            var expected = _quest.QuestRequirements.ToList();
            var result = (await Fixture.LoadQuest()).QuestRequirements.ToList();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task SkillRequirementsAreParsed()
        {
            var expected = _quest.SkillRequirements.ToList();
            var result = (await Fixture.LoadQuest()).SkillRequirements.ToList();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ItemsNeededAtStartAreParsed()
        {
            var expected = _quest.ItemsNeededAtStart.ToList();
            var result = (await Fixture.LoadQuest()).ItemsNeededAtStart.ToList();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ItemsNeededToCompleteAreParsed()
        {
            var expected = _quest.ItemsNeededToComplete;
            var result = (await Fixture.LoadQuest()).ItemsNeededToComplete;

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ItemsRecommendedAreParsed()
        {
            var expected = _quest.ItemsRecommended;
            var result = (await Fixture.LoadQuest()).ItemsRecommended;

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task QuestPointsAreParsed()
        {
            var expected = _quest.ItemsNeededAtStart;
            var result = (await Fixture.LoadQuest()).ItemsNeededAtStart;

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task RewardIsParsed()
        {
            var expected = _quest.Reward.ToList();
            var result = (await Fixture.LoadQuest()).Reward.ToList();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task StartPointIsParsed()
        {
            var expected = _quest.StartPoint.ToList();
            var result = (await Fixture.LoadQuest()).StartPoint.ToList();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ToStartIsParsed()
        {
            var expected = _quest.ToStart.ToList();
            var result = (await Fixture.LoadQuest()).ToStart.ToList();

            Assert.Equal(expected, result);
        }
    }
}
