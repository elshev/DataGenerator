using System.Collections.Generic;
using System.Linq;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;
using MongoDB.Driver;

namespace APaers.DataGen.Data.MongoDb.Providers
{
    public abstract class MongoDbDataProvider<TEntity> : IDataProvider<TEntity> 
        where TEntity: EntityBase, new()
    {
        private IMongoCollection<TEntity> collection;
        private IMongoCollection<TEntity> Collection
        {
            get
            {
                if (collection == null)
                    RefreshCollection();
                return collection;
            }
        }

        private List<TEntity> allEntities;
        protected List<TEntity> AllEntities => allEntities;
        protected abstract string CollectionName { get; }

        private void RefreshCollection()
        {
            const string connectionString = "mongodb://localhost";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase db = client.GetDatabase("DataGenTest");
            collection = db.GetCollection<TEntity>(CollectionName);
        }
        
        public virtual IEnumerable<TEntity> GetAll()
        {
            if (allEntities == null)
                RefreshAllEntities();
            return allEntities;
        }

        public TEntity GetById(object id)
        {
            return allEntities.FirstOrDefault(e => e.Id.Equals(id));
        }

        protected virtual void RefreshAllEntities()
        {
            allEntities = Collection
                .Find(FilterDefinition<TEntity>.Empty)
                .ToList();
        }
    }
}
