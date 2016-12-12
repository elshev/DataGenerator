using System.Collections.Generic;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Abstract.Repo
{
    public interface IRepo<out TEntity>
        where TEntity : EntityBase, new()
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        TEntity GetRandom();
    }
}