using System.Collections.Generic;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Abstract.Data
{
    public interface IDataProvider<out TEntity>
        where TEntity : EntityBase, new()
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
    }
}
