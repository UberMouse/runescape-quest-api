using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunescapeQuestApi.Models;
using Xunit;

namespace RunescapeQuestApi.Tests.Services
{
    public class QuestLoaderLoadQuestsTest : IClassFixture<QuestFixture>
    {
        internal QuestFixture Fixture { get; set; }

        public QuestLoaderLoadQuestsTest(QuestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task LoadQuestsReturnsAllQuestsOnPage()
        {
            var expected = new List<PartialQuest>()
            {
                new PartialQuest() { Name = "All Fired Up", Id = "0956"},
                new PartialQuest() { Name = "Animal Magnetism", Id = "0848"},
                new PartialQuest() { Name = "Another Slice of H.A.M.", Id = "0875"},
            };
            var result = await Fixture.LoadQuests();

            Assert.Equal(expected, result);
        }
    }
}
