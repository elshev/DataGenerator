using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Repo
{
    internal class FirstNamesRepo : Repo<FirstName>
    {
        public FirstNamesRepo(IDataProvider<FirstName> dataProvider) : base(dataProvider)
        {
        }
    }
}
