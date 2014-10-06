using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB;
using Extension;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Configuration;
namespace DatabaseAccess
{
    public class DatabaseManager
    {
        private string ConnectionString {get; set;}
        private string Database { get; set; }

        public DatabaseManager()
        {
            ConnectionString = ConfigurationManager.AppSettings["MongoConnection"];
            Database = ConfigurationManager.AppSettings["MongoDatabase"];
        }

        public void Insert<T>(T document)
        {
            var database = GetDatabase();
            var collection = database.GetCollection(typeof(T).Name);
            collection.Insert((T) document);               
        }

        public void Update<T>(T document)
        {
            var database = GetDatabase();
            var collection = database.GetCollection<T>(typeof(T).Name);
            collection.Save<T>(document);
        }

        public void Delete<T>(ObjectId id)
        {
            var database = GetDatabase();
            var collection = database.GetCollection<T>(typeof(T).Name);
            var query = Query.EQ("_id", id);
            collection.Remove(query);            
        }

        public T Get<T>(ObjectId id)
        {
            var database = GetDatabase();
            var collection = database.GetCollection<T>(typeof(T).Name);
            var query = Query.EQ("_id", id);
            var document = collection.FindOne(query);
            return document;
        }

        public MongoDatabase GetDatabase()
        {
            string conexaoMongo = ConnectionString;
            var client = new MongoClient(conexaoMongo);
            var server = client.GetServer();
            
            return server.GetDatabase(Database);
        }

    }
}
