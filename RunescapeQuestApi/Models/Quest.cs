using System.Collections.Generic;

namespace RunescapeQuestApi.Models
{
    public class Quest
    {
        public IEnumerable<BaseNode> Difficulty { get; set; }
        public IEnumerable<BaseNode> Length { get; set; }
        public IEnumerable<BaseNode> QuestRequirements { get; set; }
        public IEnumerable<BaseNode> SkillRequirements { get; set; }
        public IEnumerable<BaseNode> ItemsNeededAtStart { get; set; }
        public IEnumerable<BaseNode> ItemsNeededToComplete { get; set; }
        public IEnumerable<BaseNode> ItemsRecommended { get; set; }
        public IEnumerable<BaseNode> QuestPoints { get; set; }
        public IEnumerable<BaseNode> Reward { get; set; }
        public IEnumerable<BaseNode> StartPoint { get; set; }
        public IEnumerable<BaseNode> ToStart { get; set; }
    }

    public class PartialQuest
    {
        public string Name { get; set; }
        public string Id { get; set; }

        protected bool Equals(PartialQuest other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PartialQuest) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ Id.GetHashCode();
            }
        }
    }
}