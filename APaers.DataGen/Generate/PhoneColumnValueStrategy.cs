using APaers.Common;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class PhoneColumnValueStrategy : StringColumnValueStrategy<PhoneColumnInfo>
    {
        public PhoneColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }
        
        protected internal override string GetStringValue(PhoneColumnInfo columnInfo, Country country)
        {
            int resultLength = columnInfo.Format.Length;
            char[] result = new char[resultLength];
            columnInfo.Format.CopyTo(0, result, 0, resultLength);
            
            //Country code + first digit after country code is not zero
            string firstDigits = country.PhoneCode.Trim(' ', '+') + (StaticRandom.Instance.Next(9) + 1).ToString()[0];
            int fdIndex = 0;
            for (int i = 0; i < resultLength; i++)
            {
                if (result[i] != PhoneColumnInfo.Placeholder)
                    continue;
                result[i] = firstDigits[fdIndex];
                fdIndex++;
                if (fdIndex >= firstDigits.Length)
                    break;
            }

            ReplacePlaceholderToDigit(result, PhoneColumnInfo.Placeholder);
            return new string(result);
        }
    }
}