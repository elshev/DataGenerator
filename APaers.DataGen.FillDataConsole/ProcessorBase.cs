using System.Collections.Generic;
using APaers.DataGen.Entities;

namespace APaers.DataGen.FillDataConsole
{
    internal abstract class ProcessorBase
    {
        public void Save(DbCache dbCache)
        {
            SaveCollection("FirstNames", dbCache.FirstNames);
            SaveCollection("LastNames", dbCache.LastNames);
            SaveCollection("Countries", dbCache.Countries);
            SaveCollection("Regions", dbCache.Regions);
            SaveCollection("Cities", dbCache.Cities);
            SaveCollection("RandomTexts", dbCache.RandomTexts);
        }

        protected abstract void SaveCollection<TEntity>(string collectionName, List<TEntity> entities)
            where TEntity : NamedEntityBase, new();
    }
}