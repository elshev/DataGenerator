use master;

if (not exists (select 1 from sys.databases where name = 'DataGen_Test'))
begin
    create database DataGen_Test;
end;
