using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunescapeQuestApi.Models;
using RunescapeQuestApi.Services;

namespace RunescapeQuestApi.Tests.Services
{
    public class QuestFixture
    {
        public Quest Quest { get; private set; }

        public async Task<Quest> LoadQuest()
        {
            if (Quest != null)
                return Quest;

            var quest = await new QuestLoader().LoadQuest("0875");
            Quest = quest;
            return quest;
        }
    }
}
