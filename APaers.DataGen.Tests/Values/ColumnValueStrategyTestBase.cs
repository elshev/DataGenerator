using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace APaers.DataGen.Tests.Values
{
    public class ColumnValueStrategyTestBase : TestBase
    {
        protected static Country EmptyCountry { get; } = new Country();

        protected Mock<IRepoFactory> RepoFactoryMock { get; } = new Mock<IRepoFactory>();
        protected IRepoFactory RepoFactory => RepoFactoryMock.Object;
    }
}