using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace OnlineCalendarWPF
{
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
}
