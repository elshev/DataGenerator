using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class EmailColumnValueStrategy : StringColumnValueStrategy<EmailColumnInfo>
    {
        public EmailColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }
        
        protected internal override string GetStringValue(EmailColumnInfo columnInfo, Country country)
        {
            string mail = Facker.Email();
            if (columnInfo.MaxLength > 0 && mail.Length > columnInfo.MaxLength)
                mail = mail.Substring(mail.Length - columnInfo.MaxLength);
            return mail;
        }
    }
}