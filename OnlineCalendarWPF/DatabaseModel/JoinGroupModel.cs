using MongoDB.Bson.Serialization.Attributes;
using System;

namespace OnlineCalendarWPF
{
    public class JoinGroupModel
    {
        [BsonId]
        public Guid UserId { get; set; }
        [BsonId]
        public Guid GroupId { get; set; }
    }
}
