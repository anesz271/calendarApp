using MongoDB.Bson.Serialization.Attributes;
using System;

namespace OnlineCalendarWPF
{
    public class CalendarModel
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonId]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
