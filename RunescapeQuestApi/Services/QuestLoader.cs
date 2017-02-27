using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.WebSockets;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Html;
using AngleSharp.Html;
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
            {"Difficulty", GroupParser},
            {"Length",  GroupParser},
            {"QuestRequirements", QuestRequirements},
            {"SkillRequirements", SkillRequirements},
            {"ItemsNeededAtStart", ListParser},
            {"ItemsNeededToComplete", ListParser},
            {"ItemsRecommended", ListParser },
            {"QuestPoints", GroupParser},
            {"Reward", ListParser},
            {"StartPoint", GroupParser},
            {"ToStart", GroupParser}
        };

        private static IEnumerable<IWikiNode> GroupParser(IHtmlElement element)
        {
            return new List<IWikiNode>()
            {
                new GroupNode(
                    element.ChildNodes.Select<INode, IWikiNode>(node =>
                    {
                        var anchorNode = node as IHtmlAnchorElement;
                        if (anchorNode != null)
                            return new PageNode(anchorNode.Text, anchorNode.Href);

                        return new TextNode(node.TextContent.Trim());
                    }).ToArray()    
                )
            };
        }

        private static IEnumerable<IWikiNode> ListParser(IHtmlElement element)
        {
            var textCleanupActions = new List<Func<string, string>>()
            {
                text =>
                {
                    if (text.StartsWith("and", StringComparison.CurrentCultureIgnoreCase))
                        return text.TrimStart('a', 'n', 'd').Trim();
                    return text;
                },
                text => text.First().ToString().ToUpper() + text.Substring(1)
            };

            var content = element.TextContent;
            var textNodes = content
                .Split(',')
                .Select(item => item.Trim())
                .Select(item => new TextNode(textCleanupActions.Aggregate(item, (text, transform) => transform(text))))
                .OfType<IWikiNode>()
                .ToList();

            return new List<IWikiNode>()
            {
                new ListNode(textNodes)
            };
        }

        private static IEnumerable<IWikiNode> SkillRequirements(IHtmlElement element)
        {
            var skillNodes = Split(
                element
                    .ChildNodes
                    .Where(node =>
                    {
                        var castNode = node as IHtmlElement;
                        // Free floating text is not part of the DOM namespace so it won't cast
                        if (castNode == null)
                            return true;

                        return castNode.TagName != "BR";
                    }),
                size: 2
            );

            return new List<IWikiNode>()
            {
                new ListNode(
                    skillNodes
                        .Where(group => group.Count() == 2)
                        // All legitimate groups will be a TextNode and HtmlAnchorElement, this filters out the junk ones
                        .Select(group =>
                        {
                            var elements = group.ToList();

                            return new {Text = elements.First(), Anchor = (IHtmlAnchorElement) elements.Last()};
                        }).Select(group => new GroupNode(
                            new TextNode(group.Text.TextContent.Trim()),
                            new PageNode(group.Anchor.Text, group.Anchor.Href)
                        ))
                        .OfType<IWikiNode>()
                        .ToList()
               )
            };
        }

        private static IEnumerable<IWikiNode> QuestRequirements(IHtmlElement element)
        {
            var questNodes = element
                .ChildNodes
                .OfType<IHtmlElement>()
                .Where(node => node.LocalName == "a")
                .OfType<IHtmlAnchorElement>()
                .Select(node => new PageNode(node.TextContent.Trim(), node.Href));

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
            Split(
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
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to split.</param>
        /// <param name="size">The size of the smaller arrays.</param>
        /// <returns>An array containing smaller arrays.</returns>
        private static IEnumerable<IEnumerable<T>> Split<T>(IEnumerable<T> enumerable, int size)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            for (var i = 0; i < (float)array.Count() / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
    }
}