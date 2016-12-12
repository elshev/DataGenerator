using APaers.DataGen.Abstract;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;
using APaers.DataGen.Generate;
using APaers.DataGen.Log;
using APaers.DataGen.Repo;
using APaers.DataGen.SqlServer;
using Autofac;
using AddressLine1 = APaers.DataGen.Entities.AddressLine1;

namespace APaers.DataGen
{
    public class DataGenModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlServerDataGenStrategy>().Keyed<IDataGenStrategy>(SqlType.SqlServer);
            builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();
            builder.RegisterType<Logger>().As<ILog>().InstancePerLifetimeScope();


            builder.RegisterType<RandomTextRepo>().As<IRepo<RandomText>>();
            builder.RegisterType<FirstNamesRepo>().As<IRepo<FirstName>>();
            builder.RegisterType<LastNamesRepo>().As<IRepo<LastName>>();
            builder.RegisterType<CountryRepo>().As<IRepo<Country>>();
            builder.RegisterType<RegionRepo>().As<IRepo<Region>>().As<IAddressPartRepo<Region>>();
            builder.RegisterType<CityRepo>().As<IRepo<City>>().As<IAddressPartRepo<City>>();
            builder.RegisterType<Repo.AddressLine1>().As<IRepo<AddressLine1>>().As<IAddressPartRepo<AddressLine1>>();
            builder.RegisterType<AddressLine2Repo>().As<IRepo<AddressLine2>>().As<IAddressPartRepo<AddressLine2>>();
            builder.RegisterType<RepoFactory>().As<IRepoFactory>();

            builder.RegisterType<RandomTextColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.RandomText);
            builder.RegisterType<FirstNameColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.FirstName);
            builder.RegisterType<LastNameColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.LastName);
            builder.RegisterType<FullNameColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.FullName);
            builder.RegisterType<PhoneColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Phone);
            builder.RegisterType<EmailColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Email);
            builder.RegisterType<PassportNumberColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.PassportNumber);
            builder.RegisterType<PostalCodeColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.PostalCode);
            builder.RegisterType<CountryColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Country);
            builder.RegisterType<RegionColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Region);
            builder.RegisterType<CityColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.City);
            builder.RegisterType<AddressLine1ColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.AddressLine1);
            builder.RegisterType<AddressLine2ColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.AddressLine2);
            builder.RegisterType<FullAddressColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.FullAddress);
            builder.RegisterType<LatitudeLongitudeColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.LatitudeLongitude);
            builder.RegisterType<IntColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Int);
            builder.RegisterType<NumberColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Number);
            builder.RegisterType<MoneyColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Money);
            builder.RegisterType<DateTimeColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.DateTime);
            builder.RegisterType<BooleanColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Boolean);
            builder.RegisterType<AutoincColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Autoinc);
            builder.RegisterType<GuidColumnValueStrategy>().Keyed<IColumnValueStrategy>(ColumnType.Guid);
        }
    }
}
