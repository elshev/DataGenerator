using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;
using Newtonsoft.Json;

namespace APaers.DataGen.Data.Json.Providers
{
    public abstract class JsonDataProvider<TEntity> : IDataProvider<TEntity> 
        where TEntity: EntityBase, new()
    {
        private List<TEntity> allEntities;
        protected List<TEntity> AllEntities => allEntities;
        protected abstract string CollectionName { get; }

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
            string appDataFolderPath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            string filePath = Path.Combine(appDataFolderPath, CollectionName);
            filePath = Path.ChangeExtension(filePath, "json");
            string fileContent = File.ReadAllText(filePath);
            allEntities = JsonConvert.DeserializeObject<List<TEntity>>(fileContent);
        }
    }
}
