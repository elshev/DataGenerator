using System.Collections.Generic;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Abstract.Data
{
    public interface IAddressPartDataProvider<out TEntity> : IDataProvider<TEntity>
        where TEntity : AddressPart, new()
    {
        IEnumerable<TEntity> GetAll(string countryId);
    }
}