using MongoDB.Bson.Serialization.Attributes;
using System;

namespace OnlineCalendarWPF.DatabaseModel
{
    public class ReminderModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public DateTime ScheduledAt { get; set; }

    }
}
