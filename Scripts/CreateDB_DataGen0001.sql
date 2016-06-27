use master;

if (not exists (select 1 from sys.databases where name = 'DataGen_0001'))
begin
    create database DataGen_0001;
end;
