using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Consumer.Data
{
    public class MongoConnection
    {
        public static IMongoDatabase Database;
        public MongoConnection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            Database = client.GetDatabase("baseball2015");
        }
    }
}
