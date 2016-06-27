use DataGen_0001;

declare @procName sysname = 'dbo.DropAllTables'

if (isnull(objectproperty(object_id(@procName), 'IsProcedure'), 0) = 0)
begin
    execute ('create procedure ' + @procName + ' as');
end;
go

alter procedure dbo.DropAllTables
as 
begin
    declare @tableName sysname;
    declare tableCursor cursor for
        select name from sys.tables;
    open tableCursor;
    fetch next from tableCursor into @tableName;
    while (@@FETCH_STATUS = 0)
    begin
        execute('drop table ' + @tableName);
        fetch next from tableCursor into @tableName;
    end;
    close tableCursor;
    deallocate tableCursor;
end;
go
