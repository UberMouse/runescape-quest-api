using System;
using System.Collections.Generic;
using System.Linq;

namespace RunescapeQuestApi.Models
{
    public class Skill
    {
        public string Name { get; }
        public int Value { get; }

        public Skill(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public static IEnumerable<Skill> FromRequirements(string requirements)
        {
            return requirements
                .Remove(0, 1) // Remove starting new line
                .Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(pair => pair.Split(new [] { " " }, StringSplitOptions.RemoveEmptyEntries))
                .Select(splitPair => new Skill(splitPair[1], int.Parse(splitPair[0])));
        }
    }
}