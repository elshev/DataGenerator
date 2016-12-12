using APaers.DataGen.Entities;

namespace APaers.DataGen.Abstract.Repo
{
    public interface IAddressPartRepo<out TEntity> : IRepo<TEntity>
        where TEntity : EntityBase, new()
    {
        TEntity GetRandom(string countryId);
    }
}