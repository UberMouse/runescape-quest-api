using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom.Css;
using RunescapeQuestApi.Models;
using RunescapeQuestApi.Services;
using Xunit;

namespace RunescapeQuestApi.Tests.Services
{
    public class QuestLoaderTest
    {
        [Fact]
        public async Task CanLoadQuest()
        {
            var expected = new Quest()
            {
                Difficulty = new List<IWikiNode>() {new TextNode("Intermediate")},
                Length = new List<IWikiNode>() { new TextNode("Short")},
                QuestRequirements = new List<IWikiNode>()
                {
                    new ListNode(new List<IWikiNode>()
                    {
                        new PageNode("Restless Ghost, The", "http://www.runehq.com/guide.php?type=quest&id=0173"),
                        new PageNode("Ernest the Chicken", "http://www.runehq.com/guide.php?type=quest&id=0168"),
                        new PageNode("Priest in Peril", "http://www.runehq.com/guide.php?type=quest&id=0373")
                    })
                },
                SkillRequirements = new List<IWikiNode>()
                {
                    new ListNode(new List<IWikiNode>()
                    {
                        new GroupNode(
                            new TextNode("15"),
                            new PageNode("Thieving", "http://www.runehq.com/guide.php?type=skill&id=312")    
                        ),
                        new GroupNode(
                            new TextNode("18"),
                            new PageNode("Slayer", "http://www.runehq.com/guide.php?type=skill&id=312")
                        ),
                        new GroupNode(
                            new TextNode("19"),
                            new PageNode("Crafting", "http://www.runehq.com/guide.php?type=skill&id=312")
                        ),
                        new GroupNode(
                            new TextNode("30"),
                            new PageNode("Ranged", "http://www.runehq.com/guide.php?type=skill&id=312")
                        ),
                        new GroupNode(
                            new TextNode("35"),
                            new PageNode("Woodcutting", "http://www.runehq.com/guide.php?type=skill&id=312")
                        )
                    })
                },
                ItemsNeededAtStart = new List<IWikiNode>()
                {
                    new ListNode(new List<IWikiNode>()
                    {
                        new TextNode("Light source (preferably a lantern)"),
                        new TextNode("Magic or Ranged attack method"),
                        new TextNode("Armor"),
                        new TextNode("and a Weapon.")
                    })
                },
                ItemsNeededToComplete = new List<IWikiNode>()
                {
                    new ListNode(new List<IWikiNode>()
                    {
                        new TextNode("None.")
                    })
                },
                ItemsRecommended = new List<IWikiNode>()
                {
                    new GroupNode(
                        new PageNode("Dorgesh-kaan sphere", "http://www.runehq.com/database.php?type=item&id=3432"),
                        new TextNode(".")
                    )
                },
                QuestPoints = new List<IWikiNode>()
                {
                    new TextNode("1")
                },
                Reward = new List<IWikiNode>()
                {
                    new ListNode(new List<IWikiNode>()
                    {
                        new GroupNode(
                            new TextNode("3K"),
                            new PageNode("Mining", "http://www.runehq.com/guide.php?type=skill&id=337"),
                            new TextNode("XP")
                        ),
                        new GroupNode(
                            new TextNode("3K"),
                            new PageNode("Prayer", "http://www.runehq.com/guide.php?type=skill&id=295"),
                            new TextNode("XP")
                        ),
                        new PageNode("Ancient mace", "http://www.runehq.com/database.php?type=item&id=3494"),
                        new GroupNode(
                            new TextNode("access to the"),
                            new PageNode("Dorgesh-Kaan", "http://www.runehq.com/guide.php?type=city&id=917"),
                            new TextNode("-"),
                            new PageNode("Keldagrim", "http://www.runehq.com/guide.php?type=city&id=521"),
                            new TextNode("train service")
                        ),
                        new GroupNode(
                            new TextNode("the ability to buy"),
                            new PageNode("Goblin Village spheres", "http://www.runehq.com/database.php?type=item&id=25024"),
                            new TextNode("from"),
                            new PageNode("Oldak", "http://www.runehq.com/database.php?type=person&id=1411")
                        ),
                        new GroupNode(
                            new TextNode("and 2"),
                            new PageNode("Treasure Hunter", "http://www.runehq.com/guide.php?type=minigame&id=987"),
                            new TextNode("keys.")
                        )
                    })
                },
                StartPoint = new List<IWikiNode>()
                {
                    new GroupNode(
                        new TextNode("Ur-Tag's house in"),
                        new PageNode("Dorgesh-Kaan", "http://www.runehq.com/guide.php?type=city&id=917"),
                        new TextNode(".")
                    )
                },
                ToStart = new List<IWikiNode>()
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
            var result = await new QuestLoader().LoadQuest("0875");

            Assert.Equal(expected, result);
        }
    }
}
