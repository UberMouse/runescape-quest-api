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

        protected bool Equals(Quest other)
        {
            return Difficulty.Equals(other.Difficulty) && Length.Equals(other.Length) && QuestRequirements.Equals(other.QuestRequirements) && SkillRequirements.Equals(other.SkillRequirements) && ItemsNeededAtStart.Equals(other.ItemsNeededAtStart) && ItemsNeededToComplete.Equals(other.ItemsNeededToComplete) && ItemsRecommended.Equals(other.ItemsRecommended) && QuestPoints.Equals(other.QuestPoints) && Reward.Equals(other.Reward) && StartPoint.Equals(other.StartPoint) && ToStart.Equals(other.ToStart);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Quest)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Difficulty.GetHashCode();
                hashCode = (hashCode * 397) ^ Length.GetHashCode();
                hashCode = (hashCode * 397) ^ QuestRequirements.GetHashCode();
                hashCode = (hashCode * 397) ^ SkillRequirements.GetHashCode();
                hashCode = (hashCode * 397) ^ ItemsNeededAtStart.GetHashCode();
                hashCode = (hashCode * 397) ^ ItemsNeededToComplete.GetHashCode();
                hashCode = (hashCode * 397) ^ ItemsRecommended.GetHashCode();
                hashCode = (hashCode * 397) ^ QuestPoints.GetHashCode();
                hashCode = (hashCode * 397) ^ Reward.GetHashCode();
                hashCode = (hashCode * 397) ^ StartPoint.GetHashCode();
                hashCode = (hashCode * 397) ^ ToStart.GetHashCode();
                return hashCode;
            }
        }
    }
}