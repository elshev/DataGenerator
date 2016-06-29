declare @procName sysname = 'dbo.GetMetadataFromCreateTableScript'

if (isnull(objectproperty(object_id(@procName), 'IsProcedure'), 0) = 0)
begin
    execute ('create procedure ' + @procName + ' as');
end;
go

alter procedure dbo.GetMetadataFromCreateTableScript
  @CreateTableScript nvarchar(max),
  @CreatedTableName sysname output
as
begin
  exec dbo.DropAllTables;

  declare @d datetime;
  select @d = getdate();

  exec sp_executesql @CreateTableScript;

  declare @objectId int;
  select @objectId = object_id, @CreatedTableName = name
  from sys.tables
  where create_date >= @d and type = 'U'; 

  print 'Created table name = ' + @CreatedTableName;

  select ac.name, ac.column_id, st.name as type_name, ac.max_length, ac.precision, ac.scale, ac.is_nullable, ac.is_identity, ac.is_computed
  from sys.all_columns ac with (nolock)
    inner join sys.systypes st with (nolock) on ac.system_type_id = st.xusertype
  where object_id = @objectId 
  order by column_id;

  exec dbo.DropAllTables;
end;
go

