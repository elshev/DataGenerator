declare @procName sysname = '[dbo].[DropAllTables]'
if (isnull(objectproperty(object_id(@procName), 'IsProcedure'), 0) = 0)
  execute ('create procedure ' + @procName + ' as');
go

alter procedure [dbo].[DropAllTables]
as
begin
	declare @tableName sysname;
	declare tableCursor cursor for
	    select name from sys.tables with (nolock) where type = 'U';
	open tableCursor;
	fetch next from tableCursor into @tableName;
	while @@fetch_status = 0
	begin		
	  declare @dropTableScript nvarchar(1024) = 'drop table ' + @tableName;
	  --print @dropTableScript;
	  exec sp_executesql @dropTableScript;
  
	  fetch next from tableCursor into @tableName;
	end;
	close TableCursor;
	deallocate tableCursor;
end
go