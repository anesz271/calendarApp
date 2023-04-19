using MongoDB.Bson.Serialization.Attributes;
using System;

namespace OnlineCalendarWPF
{
    public class AddGroupEventModel
    {
        [BsonId]
        public Guid EventId { get; set; }
        [BsonId]
        public Guid GroupId { get; set; }
    }
}
