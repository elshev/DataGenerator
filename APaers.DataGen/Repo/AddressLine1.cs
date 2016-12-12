using APaers.DataGen.Abstract.Data;

namespace APaers.DataGen.Repo
{
    internal class AddressLine1 : AddressPartRepo<Entities.AddressLine1>
    {
        public AddressLine1(IDataProvider<Entities.AddressLine1> dataProvider) : base(dataProvider)
        {
        }

        public override Entities.AddressLine1 GetRandom()
        {
            return new Entities.AddressLine1 {Name = "Studio 870, DataGenerator Business Centre, 15 Paers Road"};
        }

        public override Entities.AddressLine1 GetRandom(string countryId)
        {
            return GetRandom();
        }
    }
}