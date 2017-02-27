using System.Collections.Generic;

namespace RunescapeQuestApi.Models
{
    public class Quest
    {
        public IEnumerable<IWikiNode> Difficulty { get; set; }
        public IEnumerable<IWikiNode> Length { get; set; }
        public IEnumerable<IWikiNode> QuestRequirements { get; set; }
        public IEnumerable<IWikiNode> SkillRequirements { get; set; }
        public IEnumerable<IWikiNode> ItemsNeededAtStart { get; set; }
        public IEnumerable<IWikiNode> ItemsNeededToComplete { get; set; }
        public IEnumerable<IWikiNode> ItemsRecommended { get; set; }
        public IEnumerable<IWikiNode> QuestPoints { get; set; }
        public IEnumerable<IWikiNode> Reward { get; set; }
        public IEnumerable<IWikiNode> StartPoint { get; set; }
        public IEnumerable<IWikiNode> ToStart { get; set; }
    }
}