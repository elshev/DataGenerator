using System.Collections.Generic;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Repo
{
    public class AddressPartRepo<TEntity> : Repo<TEntity>, IAddressPartRepo<TEntity> 
        where TEntity : AddressPart, new()
    {
        public AddressPartRepo(IDataProvider<TEntity> dataProvider) : base(dataProvider)
        {
        }

        protected IAddressPartDataProvider<TEntity> AddressPartDataProvider => DataProvider as IAddressPartDataProvider<TEntity>;

        public virtual TEntity GetRandom(string countryId)
        {
            IEnumerable<TEntity> entities = AddressPartDataProvider.GetAll(countryId);
            return GetRandomEntity(entities);
        }
    }
}