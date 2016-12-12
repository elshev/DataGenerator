using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Data.MongoDb.Providers;
using APaers.DataGen.Entities;
using Autofac;

namespace APaers.DataGen.Data.MongoDb
{
    public class MongoDbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RandomTextDataProvider>().As<IDataProvider<RandomText>>().SingleInstance();
            builder.RegisterType<FirstNameDataProvider>().As<IDataProvider<FirstName>>().SingleInstance();
            builder.RegisterType<LastNameDataProvider>().As<IDataProvider<LastName>>().SingleInstance();
            builder.RegisterType<CountryDataProvider>().As<IDataProvider<Country>>().SingleInstance();
            builder.RegisterType<CityDataProvider>().As<IDataProvider<City>>().SingleInstance();
            builder.RegisterType<RegionDataProvider>().As<IDataProvider<Region>>().SingleInstance();
        }
    }
}
