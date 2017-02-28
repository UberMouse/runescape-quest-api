using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using RunescapeQuestApi.Models;
using RunescapeQuestApi.Services;

namespace RunescapeQuestApi.Controllers
{
    public class QuestsController : ApiController
    {
        // GET api/quests
        public async Task<IEnumerable<PartialQuest>> Get()
        {
            return await new QuestLoader().LoadQuests();
        }

        // GET api/quests/0875
        public async Task<Quest> Get(string id)
        {
            return await  new QuestLoader().LoadQuest(id); ;
        }
    }
}
