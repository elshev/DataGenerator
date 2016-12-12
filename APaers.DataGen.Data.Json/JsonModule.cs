using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Data.Json.Providers;
using APaers.DataGen.Entities;
using Autofac;

namespace APaers.DataGen.Data.Json
{
    public class JsonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FirstNameDataProvider>().As<IDataProvider<FirstName>>().SingleInstance();
            builder.RegisterType<LastNameDataProvider>().As<IDataProvider<LastName>>().SingleInstance();
            builder.RegisterType<CountryDataProvider>().As<IDataProvider<Country>>().SingleInstance();
            builder.RegisterType<CityDataProvider>().As<IDataProvider<City>>().SingleInstance();
            builder.RegisterType<RegionDataProvider>().As<IDataProvider<Region>>().SingleInstance();
            builder.RegisterType<RandomTextDataProvider>().As<IDataProvider<RandomText>>().SingleInstance();
        }
    }
}
