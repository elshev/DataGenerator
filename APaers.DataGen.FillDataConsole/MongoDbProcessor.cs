using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace APaers.DataGen.FillDataConsole
{
    internal class MongoDbProcessor : ProcessorBase
    {
        private const string connectionString = "mongodb://localhost";
        private MongoClient Client { get; } = new MongoClient(connectionString);
        private IMongoDatabase Db { get; }

        public MongoDbProcessor()
        {
            Db = Client.GetDatabase("DataGenTest");
        }

        protected override void SaveCollection<TEntity>(string collectionName, List<TEntity> entities)
        {
            Db.DropCollection(collectionName);
            IMongoCollection<TEntity> collection = Db.GetCollection<TEntity>(collectionName);
            if (entities.Any())
                collection.InsertMany(entities);
            Console.WriteLine("Write to MongoDb collection of '{0}'. Count = {1}", typeof(TEntity).Name, entities.Count);
        }
    }
}