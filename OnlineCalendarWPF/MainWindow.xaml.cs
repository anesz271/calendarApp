using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Windows;

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
            db.InsertRecord("Users", new UserModel {  Name = "Esterházy Péter", Email = "estike@gmail.com"});
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
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EventModel
    {
        [BsonId]
        public  Guid Id { get; set; }   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
    }
}
