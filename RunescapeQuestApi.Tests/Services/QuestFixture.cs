using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Network;
using AngleSharp.Network.Default;
using Moq;
using RunescapeQuestApi.Models;
using RunescapeQuestApi.Services;

namespace RunescapeQuestApi.Tests.Services
{
    public class QuestFixture
    {
        private readonly QuestLoader _loader;

        public QuestFixture()
        {
            var config = Configuration.Default.WithDefaultLoader(requesters: new IRequester[] {new StubbedRequester()});
            var browsingContext = BrowsingContext.New(config);

            _loader = new QuestLoader(browsingContext);            
        }
        public async Task<Quest> LoadQuest()
        {
            var quest = await _loader.LoadQuest("0875");
       
            return quest;
        }

        public async Task<IEnumerable<PartialQuest>> LoadQuests()
        {
            var quests = await _loader.LoadQuests();

            return quests;
        }
    }

    class StubbedRequester : IRequester
    {
        public bool SupportsProtocol(string protocol)
        {
            return true;
        }

        public Task<IResponse> RequestAsync(IRequest request, CancellationToken cancel)
        {
            var file = request.Address.Path.Contains("guide") ? "quest.html" : "quests.html";
            var stream = File.Open($"Services/{file}", FileMode.Open);
            var response = new Response()
            {
                Content = stream,
                Address = request.Address,
                Headers = request.Headers
            };

            return Task.Run(() => response as IResponse);    
        }
    }
}
