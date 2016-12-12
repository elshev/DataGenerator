if exists (select name from sys.database_principals where name = 'CreateTableUser')
begin
    drop user CreateTableUser;
end;
go

if exists (select 1 from sys.database_principals where name='CreateTableRole' and Type = 'R')
begin
    drop role CreateTableRole;
end;
go

create role CreateTableRole;
go

grant alter on schema::dbo to CreateTableRole
go
grant create table to CreateTableRole;
go

deny select, update, delete, insert, execute, references on schema::dbo to CreateTableRole
go
deny select, update, delete, insert, execute, references, alter on schema::guest to CreateTableRole
go
deny select, update, delete, insert, execute, references on schema::INFORMATION_SCHEMA to CreateTableRole
go
deny select, update, delete, insert, execute, references on schema::sys to CreateTableRole
go

create user CreateTableUser without login;
go

exec sp_addrolemember 'CreateTableRole', 'CreateTableUser';
go
