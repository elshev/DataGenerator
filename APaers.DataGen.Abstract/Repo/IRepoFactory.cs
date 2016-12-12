using APaers.DataGen.Entities;

namespace APaers.DataGen.Abstract.Repo
{
    public interface IRepoFactory
    {
        IRepo<TEntity> GetRepo<TEntity>() where TEntity : EntityBase, new();
        IAddressPartRepo<TEntity> GetAddressPartRepo<TEntity>() where TEntity : EntityBase, new();
    }
}