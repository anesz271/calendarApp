using MongoDB.Bson.Serialization.Attributes;
using System;

namespace OnlineCalendarWPF
{
    public class GroupModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
