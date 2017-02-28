using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using RunescapeQuestApi.Models;
using WebGrease.Css.Extensions;
using RunescapeQuestApi.Extensions;

namespace RunescapeQuestApi.Services
{
    public class QuestLoader
    {
        private readonly IBrowsingContext _browsingContext;

        public QuestLoader(IBrowsingContext browsingContext = null)
        {
            _browsingContext = browsingContext ?? BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        }

        private readonly Dictionary<string, string> _headerMapping = new Dictionary<string, string>()
        {
            { "Difficulty:", "Difficulty" },
            { "Length:", "Length" },
            { "Quest Requirements:", "QuestRequirements" },
            { "Skill/Other Requirements:", "SkillRequirements" },
            { "Items Needed at Quest Start:", "ItemsNeededAtStart" },
            { "Items Needed to Complete Quest:", "ItemsNeededToComplete" },
            { "Items Recommended for Quest:", "ItemsRecommended" },
            { "Quest Points:", "QuestPoints" },
            { "Reward:", "Reward" },
            { "Start Point:", "StartPoint" },
            { "To Start:", "ToStart" },    
        };

        private readonly Dictionary<string, Func<IHtmlElement, IEnumerable<BaseNode>>> _sectionParseStrategies = new Dictionary<string, Func<IHtmlElement, IEnumerable<BaseNode>>>()
        {
            {"Difficulty", Parsers.GroupParser},
            {"Length", Parsers.GroupParser},
            {"QuestRequirements", Parsers.QuestRequirements},
            {"SkillRequirements", Parsers.SkillRequirements},
            {"ItemsNeededAtStart", Parsers.ListParser},
            {"ItemsNeededToComplete", Parsers.ListParser},
            {"ItemsRecommended", Parsers.ListParser },
            {"QuestPoints", Parsers.GroupParser},
            {"Reward", Parsers.ListParser},
            {"StartPoint", Parsers.GroupParser},
            {"ToStart", Parsers.GroupParser}
        };

        public async Task<Quest> LoadQuest(string id)
        {
            var address = "http://www.runehq.com/guide.php?type=quest&id=" + id;
            var document = await _browsingContext.OpenAsync(address);

            var questContent = document.QuerySelectorAll(".content-body")[0];

            return ParseQuest((IHtmlElement) questContent);
        }

        public async Task<IEnumerable<PartialQuest>> LoadQuests()
        {
            var address = "http://www.runehq.com/quests";
            var document = await _browsingContext.OpenAsync(address);

            var quests = document.QuerySelectorAll("#guideList tbody tr");

            return ParseQuests(quests);
        }

        private IEnumerable<PartialQuest> ParseQuests(IHtmlCollection<IElement> quests)
        {
            return quests
                .OfType<IHtmlTableRowElement>()
                .Select(row =>
                {
                    var questLink = row
                        .Children
                        .First()
                        .Children
                        .OfType<IHtmlAnchorElement>()
                        .First();

                    // Href looks like http://www.runehq.com/guide.php?type=quest&id=whatwewant
                    return new PartialQuest() {Name = questLink.Text, Id = questLink.Href.Split('=').Last()};
                });
        }

        private Quest ParseQuest(IHtmlElement questContent)
        {
            var quest = new Quest();
            var questProperties = quest.GetType()
                                       .GetProperties()
                                       .ToDictionary(pi => pi.Name);
            questContent
                .ChildNodes
                .OfType<IHtmlElement>()
                .Split(size: 2)
                .Select(group =>
                {
                    var elements = group.ToList();

                    return new {Heading = elements.First().TextContent.Trim(), Content = elements.Last()};
                })
                .ForEach(section =>
                {
                    if (!_headerMapping.ContainsKey(section.Heading))
                        return;

                    var name = _headerMapping[section.Heading];

                    if (_sectionParseStrategies.ContainsKey(name))
                        questProperties[name].SetValue(quest, _sectionParseStrategies[name](section.Content));
                });

            return quest;
        }
    }
}