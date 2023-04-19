using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Ink;

namespace OnlineCalendarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MongoCRUD db = new MongoCRUD("OnlineCalendar");
            //db.InsertRecord("Users", new UserModel {  Name = "Esterházy Péter", Email = "estike@gmail.com"});

            var recs = db.LoadRecords<UserModel>("Users");
            //foreach(var rec in recs)
            //{
            //    Console.WriteLine(rec.Name);
            //}

            //var rec = db.LoadRecordById<UserModel>("Users", new Guid("29f5a97e-1b8c-4ab7-873c-12e2d26a3a82"));
        
        }
    }

    public class MongoCRUD
    {
        // connection to DB
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            // initialize and connect
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        // returns all records
        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            // SELECT * 
            return collection.Find(new BsonDocument()).ToList();
        }

        // returns one record
        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        // insert or update
        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new ReplaceOptions { IsUpsert = true }); // UpdateOptions obsolate 
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            
            collection.DeleteOne(filter);
        }
    }

    public class UserModel
    {
        [BsonId] 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CalendarModel
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonId]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

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

    public class Reminder
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonId]
        public Guid UserId { get; set; }
        [BsonId]
        public Guid EventId { get; set; }
        public DateTime ScheduledAt { get; set; }

    }
}
