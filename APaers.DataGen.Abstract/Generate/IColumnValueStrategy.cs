namespace APaers.DataGen.Abstract.Generate
{
    public interface IColumnValueStrategy
    {
        string GetValue(ColumnInfo columnInfo);
        string GetValue(ColumnInfo columnInfo, object context);
    }

    public interface IColumnValueStrategy<in TColumnInfo, in TContext> : IColumnValueStrategy
        where TColumnInfo: ColumnInfo
        where TContext: class
    {
        string GetValue(TColumnInfo columnInfo);
        string GetValue(TColumnInfo columnInfo, TContext context);
    }
}