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
namespace DatabaseAccess
{
    public class DatabaseManager
    {
        private string ConnectionString {get; set;}
        private string Database { get; set; }

        public DatabaseManager()
        {
            ConnectionString = "mongodb://localhost";
            Database = "Teste";
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

        public T Get<T>(ObjectId id)
        {
            var database = GetDatabase();
            var collection = database.GetCollection<T>(typeof(T).Name);
            var query = Query.EQ("Id", id);
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
