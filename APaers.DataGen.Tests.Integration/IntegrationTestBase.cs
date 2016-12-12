using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using APaers.DataGen.Abstract;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.AppBase;
using APaers.DataGen.Entities;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Integration
{
    public class IntegrationTestBase
    {
        protected virtual bool IsTransactional => false;
        protected TransactionScope TransactionScope { get; private set; }
        protected SqlConnection Connection { get; private set; }

        internal class TestConnectionStringProvider : IConnectionStringProvider
        {
            public string GetConnectionString()
            {
                return ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            }
        }

        public TestContext TestContext { get; set; }

        private ILifetimeScope LifetimeScope { get; set; }

        private static void InitializeContainer(ContainerBuilder builder)
        {
            builder.RegisterType<TestConnectionStringProvider>().As<IConnectionStringProvider>();
        }

        protected IDataGenStrategy CreateSqlServerDataGenStrategy()
        {
            return LifetimeScope.ResolveKeyed<IDataGenStrategy>(SqlType.SqlServer);
        }

        protected static SortedList<string, Country> Countries { get; set; }
        protected static SortedList<string, Region> Regions { get; set; }
        protected static SortedList<string, City> Cities { get; set; }

        private static SortedList<string, T> GetSortedList<T>(IDataProvider<T> dataProvider)
            where T: NamedEntityBase, new()
        {
            SortedList<string, T> result = new SortedList<string, T>();
            foreach (T entity in dataProvider.GetAll())
            {
                if (result.ContainsKey(entity.Name)) continue;
                result[entity.Name] = entity;
            }
            return result;
        }

        public static void Initialize(TestContext context)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", @"..\..\..\APaers.DataGen.Api\App_Data");
            Countries = GetSortedList(Resolve<IDataProvider<Country>>());
            Regions = GetSortedList(Resolve<IDataProvider<Region>>());
            Cities = GetSortedList(Resolve<IDataProvider<City>>());
        }

        protected static void ClassInitializeBase(TestContext testContext)
        {
            Container.Initialize(null, InitializeContainer);
        }

        protected static void ClassCleanupBase()
        {
            DisposeContainer();
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            LifetimeScope = Container.InternalContainer.BeginLifetimeScope();
            if (IsTransactional)
                TransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);

            string connectionString = new TestConnectionStringProvider().GetConnectionString();
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();

            if (IsTransactional)
                TransactionScope.Dispose();
        }

        protected static T Resolve<T>()
        {
            return Container.InternalContainer.Resolve<T>();
        }

        private static void DisposeContainer()
        {
            Container.InternalContainer.Dispose();
        }

        protected void Log(string value, string fileName = null)
        {
            if (fileName == null)
                fileName = TestContext.TestName;
            string filePath = Path.Combine(TestContext.TestResultsDirectory, fileName);

            TestContext.WriteLine(value + "\n");
            File.AppendAllText(filePath, value + "\n");
        }

        protected void ExecuteNonQuery(string script)
        {
            using (SqlCommand cmd = new SqlCommand(script, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        protected DataTable ExecuteCommand(string commandName, CommandType cmdType, SqlParameter[] param = null)
        {
            DataTable result = new DataTable();
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = commandName;
            if (param != null)
                cmd.Parameters.AddRange(param);
            Connection.Open();
            try
            {
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(result);
                }
            }
            finally
            {
                cmd.Dispose();
                Connection.Close();
            }
            return result;
        }

        protected static bool IsValidEmail(string email)
        {
            return TestBase.IsValidEmail(email);
        }

    }
}