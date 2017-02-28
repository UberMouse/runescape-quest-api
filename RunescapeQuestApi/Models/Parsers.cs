using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using RunescapeQuestApi.Extensions;

namespace RunescapeQuestApi.Models
{
    public class Parsers
    {
        public static IEnumerable<BaseNode> GroupParser(IHtmlElement element)
        {
            return new List<BaseNode>()
            {
                new GroupNode(
                    element.ChildNodes.Select<INode, BaseNode>(node =>
                    {
                        var anchorNode = node as IHtmlAnchorElement;
                        if (anchorNode != null)
                            return new PageNode(anchorNode.Text, anchorNode.Href);

                        return new TextNode(node.TextContent.Trim());
                    }).ToArray()    
                )
            };
        }

        public static IEnumerable<BaseNode> ListParser(IHtmlElement element)
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
                .OfType<BaseNode>()
                .ToList();

            return new List<BaseNode>()
            {
                new ListNode(textNodes)
            };
        }

        public static IEnumerable<BaseNode> SkillRequirements(IHtmlElement element)
        {
            var skillNodes = element
                .ChildNodes
                .Where(node =>
                {
                    var castNode = node as IHtmlElement;
                    // Free floating text is not part of the DOM namespace so it won't cast
                    if (castNode == null)
                        return true;

                    return castNode.TagName != "BR";
                })
                .Split(size: 2);

            return new List<BaseNode>()
            {
                new ListNode(
                    skillNodes
                        .Where(group => @group.Count() == 2)
                        // All legitimate groups will be a TextNode and HtmlAnchorElement, this filters out the junk ones
                        .Select(group =>
                        {
                            var elements = @group.ToList();

                            return new {Text = elements.First(), Anchor = (IHtmlAnchorElement) elements.Last()};
                        }).Select(group => new GroupNode(
                            new TextNode(@group.Text.TextContent.Trim()),
                            new PageNode(@group.Anchor.Text, @group.Anchor.Href)
                        ))
                        .OfType<BaseNode>()
                        .ToList()
                )
            };
        }

        public static IEnumerable<BaseNode> QuestRequirements(IHtmlElement element)
        {
            var questNodes = element
                .ChildNodes
                .OfType<IHtmlElement>()
                .Where(node => node.LocalName == "a")
                .OfType<IHtmlAnchorElement>()
                .Select(node => new PageNode(node.TextContent.Trim(), node.Href));

            return new List<BaseNode>()
            {
                new ListNode(questNodes.OfType<BaseNode>().ToList())
            };
        }
    }
}