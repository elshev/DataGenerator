using System.Collections.Generic;
using System.Linq;
using APaers.Common;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;
using Autofac;

namespace APaers.DataGen.Repo
{
    public abstract class Repo<TEntity> : IRepo<TEntity>
        where TEntity: EntityBase, new()
    {
        protected Repo(IDataProvider<TEntity> dataProvider)
        {
            DataProvider = dataProvider;
        }
        
        protected IDataProvider<TEntity> DataProvider { get; }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DataProvider.GetAll();
        }

        public TEntity GetById(object id)
        {
            return DataProvider.GetById(id);
        }

        public virtual TEntity GetRandom()
        {
            IEnumerable<TEntity> allEntities = GetAll();
            return GetRandomEntity(allEntities);
        }

        protected TEntity GetRandomEntity(IEnumerable<TEntity> entities)
        {
            if (entities == null) return null;
            List<TEntity> list = entities.ToList();
            int count = list.Count;
            if (count == 0) return null;
            int index = StaticRandom.Instance.Next(count);
            return list[index];
        }
    }

    internal class RepoFactory : IRepoFactory
    {
        private ILifetimeScope LifetimeScope { get; }

        public RepoFactory(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope;
        }

        public IRepo<TEntity> GetRepo<TEntity>()
            where TEntity : EntityBase, new()
        {
            return LifetimeScope.Resolve<IRepo<TEntity>>();
        }

        public IAddressPartRepo<TEntity> GetAddressPartRepo<TEntity>() where TEntity : EntityBase, new()
        {
            return LifetimeScope.Resolve<IAddressPartRepo<TEntity>>();
        }
    }
}