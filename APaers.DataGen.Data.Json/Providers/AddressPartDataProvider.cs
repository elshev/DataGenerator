using System;
using System.Collections.Generic;
using System.Linq;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;
using MongoDB.Bson;

namespace APaers.DataGen.Data.Json.Providers
{
    public abstract class AddressPartDataProvider<TEntity> : JsonDataProvider<TEntity>, IAddressPartDataProvider<TEntity>
        where TEntity : AddressPart, new()
    {
        private Dictionary<string, List<TEntity>> allCountryEntities;

        protected override void RefreshAllEntities()
        {
            base.RefreshAllEntities();
            allCountryEntities = new Dictionary<string, List<TEntity>>();
            foreach (TEntity entity in AllEntities)
            {
                string countryId = entity.CountryId;
                if (!allCountryEntities.ContainsKey(countryId))
                    allCountryEntities.Add(countryId, new List<TEntity>());
                List<TEntity> countryEntities = allCountryEntities[countryId];
                countryEntities.Add(entity);
            }
        }

        public IEnumerable<TEntity> GetAll(string countryId)
        {
            if (string.IsNullOrWhiteSpace(countryId))
                throw new ArgumentException($"Parameter {nameof(countryId)} shouldn't be empty", nameof(countryId));
            if (allCountryEntities == null)
                RefreshAllEntities();
            if (allCountryEntities == null || !allCountryEntities.ContainsKey(countryId))
                return Enumerable.Empty<TEntity>();
            return allCountryEntities[countryId];
        }
    }
}