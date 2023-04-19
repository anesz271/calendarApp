using MongoDB.Bson.Serialization.Attributes;
using System;

namespace OnlineCalendarWPF
{
    public class EventModel
    {
        [BsonId]
        public  Guid Id { get; set; }
        [BsonId]
        public Guid UserId { get; set; }
        [BsonId]
        public Guid CalendarId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
