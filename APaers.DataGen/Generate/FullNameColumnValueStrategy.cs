using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class FullNameColumnValueStrategy : StringColumnValueStrategy<FullNameColumnInfo>
    {
        public FullNameColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(FullNameColumnInfo columnInfo, Country country)
        {
            string firstName = GetFirstName();
            string middleName = GetFirstName();
            char middleChar = middleName[0];
            string lastName = GetLastName();

            switch (columnInfo.FullNameFormat)
            {
                case FullNameFormat.LastFirst:
                    return $"{lastName} {firstName}";
                case FullNameFormat.FirstMLast:
                    return $"{firstName} {middleChar}. {lastName}";
                case FullNameFormat.LastMFirst:
                    return $"{lastName} {middleChar}. {firstName}";
                case FullNameFormat.FMLast:
                    return $"{firstName[0]}.{middleChar}. {lastName}";
                case FullNameFormat.LastFM:
                    return $"{lastName} {firstName[0]}.{middleChar}.";
                default:
                    return $"{firstName} {lastName}";
            }
        }
    }
}