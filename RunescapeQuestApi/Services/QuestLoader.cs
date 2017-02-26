using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.WebSockets;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using RunescapeQuestApi.Models;
using WebGrease.Css.Extensions;

namespace RunescapeQuestApi.Services
{
    public class QuestLoader
    {
        private readonly Dictionary<string, string> headerMapping = new Dictionary<string, string>()
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

        private readonly Dictionary<string, Func<IHtmlElement, IEnumerable<IWikiNode>>> _sectionParseStrategies = new Dictionary<string, Func<IHtmlElement, IEnumerable<IWikiNode>>>()
        {
            {"Difficulty",  element => new List<IWikiNode>() { new TextNode(element.TextContent) }},
            {"Length",  element => new List<IWikiNode>() { new TextNode(element.TextContent) }},
            {"QuestRequirements", QuestRequirements}
        };

        private static IEnumerable<IWikiNode> QuestRequirements(IHtmlElement element)
        {
            var questNodes = element
                .ChildNodes
                .OfType<IHtmlElement>()
                .Where(node => node.LocalName == "a")
                .OfType<IHtmlAnchorElement>()
                .Select(node => new PageNode(node.TextContent, node.Href));

            return new List<IWikiNode>()
            {
                new ListNode(questNodes.OfType<IWikiNode>().ToList())
            };
        }

        public async Task<Quest> LoadQuest(string id)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "http://www.runehq.com/guide.php?type=quest&id=" + id;
            var document = await BrowsingContext.New(config).OpenAsync(address);

            var questContent = document.QuerySelectorAll(".content-body")[1];

            return ParseQuest((IHtmlElement) questContent);
        }

        private Quest ParseQuest(IHtmlElement questContent)
        {
            var quest = new Quest();
            var questProperties = quest.GetType()
                                       .GetProperties()
                                       .ToDictionary(pi => pi.Name);
            Split<IHtmlElement>(
                questContent.ChildNodes.OfType<IHtmlElement>(), 
                size: 2
            ).Select(group =>
            {
                var elements = group.ToList();

                return new {Heading = elements.First().TextContent, Content = elements.Last()};
            })
            .ForEach(section =>
            {
                if (!headerMapping.ContainsKey(section.Heading))
                    return;

                var name = headerMapping[section.Heading];

                if (_sectionParseStrategies.ContainsKey(name)) 
                    questProperties[name].SetValue(quest, _sectionParseStrategies[name](section.Content));             
             });

            return quest;
        }

        /// <summary>
        /// Splits an array into several smaller arrays.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to split.</param>
        /// <param name="size">The size of the smaller arrays.</param>
        /// <returns>An array containing smaller arrays.</returns>
        private IEnumerable<IEnumerable<T>> Split<T>(IEnumerable<T> array, int size)
        {
            for (var i = 0; i < (float)array.Count() / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
    }
}