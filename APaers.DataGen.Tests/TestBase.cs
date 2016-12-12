using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests
{
    public class TestBase
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}