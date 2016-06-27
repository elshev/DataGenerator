select * from sys.tables;
select * from sys.columns where object_id = 581577110;

create table Test01
(
	Id int identity(1,1) not null,
	Name varchar(128) not null,
	Date datetime null
)

exec dbo.DropAllTables;