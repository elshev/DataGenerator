using System.Configuration;
using System.Data.SqlClient;
using APaers.DataGen.Abstract.Data;

namespace APaers.DataGen
{
    internal class ConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
        }
    }
}