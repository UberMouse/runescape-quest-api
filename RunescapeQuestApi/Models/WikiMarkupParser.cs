﻿using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using static AngleSharp.Dom.NodeType;

namespace RunescapeQuestApi.Models
{
    public class WikiMarkupParser
    {
        public IEnumerable<BaseNode> Parse(INodeList nodes)
        {
            return nodes.Select(ConstructNode).OfType<BaseNode>();
        }

        private BaseNode ConstructNode(INode node)
        {
            switch (node.NodeType)
            {
                case Element:
                    if (!(node is IHtmlAnchorElement))
                        return null;

                    var anchor = node as IHtmlAnchorElement;
                    var href = anchor.Attributes.GetNamedItem("href").Value;
                    
                    if (href.Contains("/wiki/"))
                        return new PageNode(node.TextContent, href);

                    return null;
                case Text:
                    if (string.IsNullOrWhiteSpace(node.TextContent))
                        return null;

                    return new TextNode(node.TextContent.Trim());
                default:
                    return null;
            }
        }
    }
}