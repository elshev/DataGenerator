declare @createTableScript nvarchar(max) = '
create table dbo.Test01 (
  Id int identity(1,1) not null,
  Guid uniqueidentifier not null,
  Name varchar(128) not null,
  CharColumn char(128) null,
  IntColumn int null,
  NVarcharName nvarchar(256) null,
  FloatColumn float null,
  RealColumn real null,
  DecimalColumn decimal(5,2),
  NumericColumn numeric(10,5),
  MoneyColumn money,
  SmallMoneyColumn smallmoney,
);
';

declare @createdTableName sysname
exec dbo.GetMetadataFromCreateTableScript @CreateTableScript = @createTableScript, @CreatedTableName = @createdTableName out;

select @createdTableName;

--create table dbo.Test01 (Id int identity(1,1) not null, Name varchar(128) not null);
/*
insert into dbo.Test01 (Name) values
('Name 1'), 
('Name 2');
*/
/*declare @objectId int;
select @objectId = object_id
from sys.tables
where type = 'U'; 

select @objectId

select *, ac.name, ac.column_id, st.name as type_name, ac.max_length, ac.precision, ac.scale, ac.is_nullable, ac.is_identity, ac.is_computed
from sys.all_columns ac with (nolock)
  inner join sys.systypes st with (nolock) on ac.system_type_id = st.xtype
where object_id = @objectId 
order by ac.column_id;

select rm.role_principal_id, dp.name, rm.member_principal_id, dp1.name, *
from sys.database_role_members rm
  inner join sys.database_principals dp on rm.role_principal_id = dp.principal_id
  inner join sys.database_principals dp1 on rm.member_principal_id = dp1.principal_id*/
