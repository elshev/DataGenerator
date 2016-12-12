using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;
using Moq;

namespace APaers.DataGen.Tests.Values
{
    public class AddressColumnValueStrategyTestBase : ColumnValueStrategyTestBase
    {
        protected const string CountryName = "Banana Democracy Republic";

        private readonly Mock<IRepo<Country>> countryRepo = new Mock<IRepo<Country>>();

        protected override void Initialize()
        {
            base.Initialize();
            countryRepo.Setup(repo => repo.GetRandom()).Returns(new Country { Name = CountryName });
            RepoFactoryMock.Setup(rf => rf.GetRepo<Country>()).Returns(countryRepo.Object);
        }

    }
}