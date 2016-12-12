using System.Globalization;
using APaers.Common;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class LatitudeLongitudeColumnValueStrategy : StringColumnValueStrategy<LatitudeLongitudeColumnInfo>
    {
        public LatitudeLongitudeColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(LatitudeLongitudeColumnInfo columnInfo, Country country)
        {
            double latitude = StaticRandom.Instance.NextDouble(-90, 90);
            double longitude = StaticRandom.Instance.NextDouble(-180, 180);
            return latitude.ToString("#0.00######", CultureInfo.InvariantCulture) + ", " +
                   longitude.ToString("##0.00######", CultureInfo.InvariantCulture);
        }
    }
}