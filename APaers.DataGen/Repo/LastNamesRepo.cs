using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Repo
{
    internal class LastNamesRepo : Repo<LastName>
    {
        public LastNamesRepo(IDataProvider<LastName> dataProvider) : base(dataProvider)
        {
        }
    }
}