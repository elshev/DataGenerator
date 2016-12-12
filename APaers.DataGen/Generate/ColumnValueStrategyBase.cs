using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Generate
{
    internal abstract class ColumnValueStrategyBase<TColumnInfo, TContext> : IColumnValueStrategy<TColumnInfo, TContext> 
        where TColumnInfo : ColumnInfo
        where TContext: class
    {
        protected IRepoFactory RepoFactory { get; private set; }

        protected ColumnValueStrategyBase(IRepoFactory repoFactory)
        {
            RepoFactory = repoFactory;
        }

        public string GetValue(TColumnInfo columnInfo)
        {
            return GetValue(columnInfo, null);
        }

        public abstract string GetValue(TColumnInfo columnInfo, TContext context);

        public string GetValue(ColumnInfo columnInfo)
        {
            return GetValue(columnInfo, null);
        }

        public string GetValue(ColumnInfo columnInfo, object context)
        {
            return GetValue(columnInfo as TColumnInfo, context as TContext);
        }
    }
}