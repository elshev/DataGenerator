revert;
revert;
revert;
if exists (select name from sys.database_principals where name = 'u1')
    drop user u1;

create user u1 without login;
grant alter on schema::dbo to u1
grant create table to u1;

create table t2 (Id int not null);
print 'suser = ' + suser_name() + '; user = ' + user_name();

--execute as user = 'CreateTableUser';
execute as user = 'u1';
print 'suser = ' + suser_name() + '; user = ' + user_name();
create table t1 (Id int not null);

drop table t1;
drop table t2;

revert;
revert;
revert;

/*insert into t1 (Id) values (1);
select * from t1;


revert;
print 'suser = ' + suser_name() + '; user = ' + user_name();
insert into t1 (Id) values (1);
if exists (select 1 from t1)
    print 't1 exists';
drop table t1;
*/