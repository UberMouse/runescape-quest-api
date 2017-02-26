using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AngleSharp;
using RunescapeQuestApi.Models;

namespace RunescapeQuestApi.Services
{
    public class QuestLoader
    {
        public async Task<Quest> LoadQuest(string id)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "http://runescape.wikia.com/wiki/" + id;
            const string articleSelector = "#WikiaArticle";

            var document = await BrowsingContext.New(config).OpenAsync(address);
            var article = document.QuerySelector(articleSelector);

            return null;
        }
    }
}