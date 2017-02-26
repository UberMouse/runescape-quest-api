using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using AngleSharp.Dom;

namespace RunescapeQuestApi.Models
{
    public class Quest
    {
        private readonly string _id;
        private readonly QuestDetails _details;

        public Quest(string id, QuestDetails details)
        {
            _id = id;
            _details = details;
        }

//        public string StartPoint()
//        {
//            return _questDetails["Start point"];
//        }
//
//        public bool MemberRequirement()
//        {
//            return !_questDetails["Member requirement"].Contains("Free");
//        }
//
//        public string Difficulty()
//        {
//            return _questDetails["Official difficulty"];
//        }
//
//        public string Length()
//        {
//            return _questDetails["Length"];
//        }
//
//        public IEnumerable<Skill> Requirements()
//        {
//            return Skill.FromRequirements(_questDetails["Requirements"]);
//        }
//
//        public string ItemsRequired()
//        {
//            return _questDetails["Items required"];
//        }
//
//        public string Enemies()
//        {
//            return _questDetails["Enemies to defeat"];
//        }
    }
}