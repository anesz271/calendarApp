using MongoDB.Bson.Serialization.Attributes;
using System;

namespace OnlineCalendarWPF.DatabaseModel
{
    public class UserModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
