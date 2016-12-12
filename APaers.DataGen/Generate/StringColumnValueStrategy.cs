using APaers.Common;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal abstract class StringColumnValueStrategy<TColumnInfo> : ColumnValueStrategyBase<TColumnInfo, Country>
        where TColumnInfo : ColumnInfo
    {
        protected StringColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public override string GetValue(TColumnInfo columnInfo, Country country = null)
        {
            if (columnInfo.IsNextValueNull())
                return "null";
            country = country ?? RepoFactory.GetRepo<Country>().GetRandom();
            string value = GetStringValue(columnInfo, country);
            if (columnInfo.MaxLength > 0 && value.Length > columnInfo.MaxLength)
                value = value.Substring(0, columnInfo.MaxLength);
            return value;
        }

        protected internal abstract string GetStringValue(TColumnInfo columnInfo, Country country);

        protected static char GetRandomDigit()
        {
            return StaticRandom.Instance.Next(10).ToString()[0];
        }

        protected static string GenerateRandomDigitString(int length)
        {
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = GetRandomDigit();
            }
            return new string(result);
        }

        protected static void ReplacePlaceholderToDigit(char[] format, char placeholder)
        {
            for (int i = 0; i < format.Length; i++)
            {
                if (format[i] == placeholder)
                    format[i] = GetRandomDigit();
            }
        }

        protected static string ReplacePlaceholderToDigit(string format, char placeholder)
        {
            char[] result = new char[format.Length];
            format.CopyTo(0, result, 0, format.Length);
            ReplacePlaceholderToDigit(result, placeholder);
            return new string(result);
        }

        protected internal string GetFirstName()
        {
            return RepoFactory.GetRepo<FirstName>().GetRandom().Name;
        }

        protected internal string GetLastName()
        {
            return RepoFactory.GetRepo<LastName>().GetRandom().Name;
        }

        protected internal string GetCountryName(Country country)
        {
            return country.Name;
        }

        protected internal string GetRegionName(Country country)
        {
            Region region = RepoFactory.GetAddressPartRepo<Region>().GetRandom(country.Id);
            return region == null ? string.Empty : region.Name;
        }

        protected internal string GetCityName(Country country)
        {
            City city = RepoFactory.GetAddressPartRepo<City>().GetRandom(country.Id);
            return city == null ? string.Empty : city.Name;
        }

        protected internal string GetAddressLine1(Country country)
        {
            return "Studio 870, DataGenerator Business Centre, 15 Paers Road";
        }

        protected internal string GetAddressLine2(Country country)
        {
            return "room #156";
        }
    }
}