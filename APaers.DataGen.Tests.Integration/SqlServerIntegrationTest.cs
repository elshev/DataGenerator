using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using APaers.DataGen.Abstract.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Integration
{
    [TestClass]
    public class SqlServerIntegrationTest : IntegrationTestBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ClassInitializeBase(context);
            Initialize(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            ClassCleanupBase();
        }

        private string tableToDrop;
        
        [TestCleanup]
        public override void TestCleanup()
        {
            DropTable(tableToDrop);
            base.TestCleanup();
        }

        private void DropTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return;
            string[] parts = tableName.Split('.');
            ExecuteNonQuery($"if exists (select 1 from sysobjects where type = 'U' and name = '{parts[parts.Length - 1]}') " +
                            $"drop table {tableName};");
        }

        [TestMethod]
        public async Task TestGenerateFromScript_NullArgument()
        {
            // Arrange
            IDataGenStrategy strategy = CreateSqlServerDataGenStrategy();
            // Act
            TableInfo tableInfo = await strategy.GetTableInfoAsync(null);
            // Assert
            Assert.IsNull(tableInfo);
        }

        [TestMethod]
        public async Task TestGenerateFromScript()
        {
            // Arrange
            const string tableName = "TestDB.dbo.Test01";
            IDataGenStrategy strategy = CreateSqlServerDataGenStrategy();
            // Act
            TableInfo tableInfo = await strategy.GetTableInfoAsync(
                $"create table {tableName} (" +
                "Id int IDENTITY(55,2) NOT NULL, " +
                "[Name] [varchar](128) NOT NULL, " +
                "NVarCharName nvarchar(256) null" +
                ") ON [PRIMARY]");
            // Assert
            Assert.AreEqual(tableName, tableInfo.Name);
            var columnList = tableInfo.Columns.ToList();
            Assert.AreEqual(3, columnList.Count);

            int columnIndex = 0;
            ColumnInfo columnInfo = columnList[0];
            Assert.AreEqual("Id", columnInfo.Name);
            Assert.IsTrue(columnInfo is AutoincColumnInfo);
            Assert.AreEqual(true, columnInfo.IsIdentity);
            Assert.AreEqual(55, columnInfo.IdentitySeed);
            Assert.AreEqual(2, columnInfo.IdentityIncrement);
            Assert.AreEqual(0, columnInfo.MaxLength);
            Assert.AreEqual(0, columnInfo.Precision);
            Assert.AreEqual(0, columnInfo.Scale);
            Assert.IsTrue(columnInfo.IsIdentity);
            Assert.IsFalse(columnInfo.IsComputed);
            Assert.IsFalse(columnInfo.IsNullable);
            columnIndex++;
            columnInfo = columnList[columnIndex];
            Assert.AreEqual("[Name]", columnInfo.Name);
            Assert.IsTrue(columnInfo is RandomTextColumnInfo);
            Assert.AreEqual(128, columnInfo.MaxLength);
            Assert.AreEqual(0, columnInfo.Precision);
            Assert.AreEqual(0, columnInfo.Scale);
            Assert.IsFalse(columnInfo.IsIdentity);
            Assert.IsFalse(columnInfo.IsComputed);
            Assert.IsFalse(columnInfo.IsNullable);
            columnIndex++;
            columnInfo = columnList[columnIndex];
            Assert.AreEqual("NVarCharName", columnInfo.Name);
            Assert.IsTrue(columnInfo is RandomTextColumnInfo);
            Assert.AreEqual(256, columnInfo.MaxLength);
            Assert.AreEqual(0, columnInfo.Precision);
            Assert.AreEqual(0, columnInfo.Scale);
            Assert.IsFalse(columnInfo.IsIdentity);
            Assert.IsFalse(columnInfo.IsComputed);
            Assert.IsTrue(columnInfo.IsNullable);
        }

        [TestMethod]
        public async Task TestGenerateInsertScript()
        {
            // Arrange
            const string tableName = "dbo.Test01";
            tableToDrop = tableName;
            string createTableScript =
                $"create table {tableName} (" +
                "Id int identity(1,1) NOT NULL, " +
                "[Name] [varchar](128) NOT NULL," +
                "IntColumn int null" +
                ") ON [PRIMARY]";
            InsertScriptGenerationOptions generationOptions = new InsertScriptGenerationOptions { RowCount = 8 };
            IDataGenStrategy strategy = CreateSqlServerDataGenStrategy();
            // Act
            TableInfo tableInfo = await strategy.GetTableInfoAsync(createTableScript);
            string insertScript = await strategy.GenerateInsertScriptAsync(tableInfo, generationOptions);
            // Assert
            Log(createTableScript);
            Log(insertScript);
            Assert.IsTrue(tableInfo.Columns.Count > 0);
            string createInsertScript = createTableScript + Environment.NewLine + insertScript;
            ExecuteNonQuery(createInsertScript);

            using (SqlCommand cmd = new SqlCommand($"select * from {tableName}", Connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    const int colId = 0;
                    const int colName = 1;
                    Assert.AreEqual(3, reader.FieldCount);
                    Assert.AreEqual("int", reader.GetDataTypeName(colId));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colName));
                    int rowCount = 0;
                    while (reader.Read())
                    {
                        rowCount++;
                        Assert.IsFalse(reader.IsDBNull(colId));
                        Assert.AreEqual(rowCount, reader.GetInt32(colId));
                        Assert.IsFalse(reader.IsDBNull(colName));
                    }
                    Assert.AreEqual(generationOptions.RowCount, rowCount);
                }
            }
        }

        [TestMethod]
        public async Task TestGenerateInsertScript_IdColumns()
        {
            // Arrange
            const string tableName = "dbo.Test01";
            tableToDrop = tableName;
            string createTableScript =
                $"create table {tableName} (" +
                "Id int not null" +
                ") ON [PRIMARY]";
            InsertScriptGenerationOptions generationOptions = new InsertScriptGenerationOptions { RowCount = 10 };
            IDataGenStrategy strategy = CreateSqlServerDataGenStrategy();
            // Act
            TableInfo tableInfo = await strategy.GetTableInfoAsync(createTableScript);
            string insertScript = await strategy.GenerateInsertScriptAsync(tableInfo, generationOptions);
            // Assert
            Log(createTableScript);
            Log(insertScript);
            Assert.IsTrue(tableInfo.Columns.Count > 0);
            string createInsertScript = createTableScript + Environment.NewLine + insertScript;
            ExecuteNonQuery(createInsertScript);
            using (SqlCommand cmd = new SqlCommand($"select * from {tableName}", Connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    const int colId = 0;
                    Assert.AreEqual("int", reader.GetDataTypeName(colId));
                    int id = 1;
                    while (reader.Read())
                    {
                        Assert.IsFalse(reader.IsDBNull(colId));
                        Assert.AreEqual(id, reader.GetInt32(colId));
                        id++;
                    }
                }
            }
        }

        [TestMethod]
        public async Task TestGenerateInsertScript_DifferentColumnTypes_DefaultFormats()
        {
            // Arrange
            const string tableName = "dbo.Test01";
            tableToDrop = tableName;
            string createTableScript =
                    $"create table {tableName} (" +
                    "Id int identity(1,1) not null, " +
                    "SomeUniqueId uniqueidentifier not null, " +
                    "SomeGuid varchar(64) not null, " +
                    "Country varchar(128) not null, " +
                    "RegionName nvarchar(128) not null, " +
                    "City nvarchar(128) not null, " +
                    "AddressLine1 varchar(256) not null, " +
                    "AddressLine2 varchar(256) not null, " +
                    "LatitudeLongitude varchar(32) not null, " +
                    "SomeTextColumn varchar(128) not null, " +
                    "PersonFullName varchar(16) not null, " +
                    "Email varchar(64) not null, " +
                    "Telephone varchar(16) not null, " +
                    "PassportNo varchar(16) not null, " +
                    "PostalCode varchar(12) not null, " +
                    "WholeValue int not null, " +
                    "DecimalValue decimal not null, " +
                    "FloatValue float not null, " +
                    "BoolValue bit not null, " +
                    "DateTimeValue datetime not null, " +
                    ") on [primary];";
            InsertScriptGenerationOptions generationOptions = new InsertScriptGenerationOptions { RowCount = 10 };
            IDataGenStrategy strategy = CreateSqlServerDataGenStrategy();
            // Act
            TableInfo tableInfo = await strategy.GetTableInfoAsync(createTableScript);
            string insertScript = await strategy.GenerateInsertScriptAsync(tableInfo, generationOptions);
            // Assert
            Log(createTableScript);
            Log(insertScript);
            Assert.IsTrue(tableInfo.Columns.Count > 0);
            string createInsertScript = createTableScript + Environment.NewLine + insertScript;
            ExecuteNonQuery(createInsertScript);

            using (SqlCommand cmd = new SqlCommand($"select * from {tableName}", Connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int col = 0;
                    int colId = col++;
                    int colGuid = col++;
                    int colGuidString = col++;
                    int colCountry = col++;
                    int colRegion = col++;
                    int colCity = col++;
                    int colAddressLine1 = col++;
                    int colAddressLine2 = col++;
                    int colLatitudeLongitude = col++;
                    int colRandomText = col++;
                    int colFullName = col++;
                    int colEmail = col++;
                    int colPhone = col++;
                    int colPassport = col++;
                    int colPostalCode = col++;
                    int colInt = col++;
                    int colDecimal = col++;
                    int colFloat = col++;
                    int colBoolean = col++;
                    int colDateTime = col++;

                    Assert.AreEqual(col, reader.FieldCount);

                    Assert.AreEqual("int", reader.GetDataTypeName(colId));
                    Assert.AreEqual("uniqueidentifier", reader.GetDataTypeName(colGuid));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colGuidString));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colCountry));
                    Assert.AreEqual("nvarchar", reader.GetDataTypeName(colRegion));
                    Assert.AreEqual("nvarchar", reader.GetDataTypeName(colCity));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colAddressLine1));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colAddressLine2));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colLatitudeLongitude));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colRandomText));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colFullName));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colEmail));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colPhone));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colPassport));
                    Assert.AreEqual("varchar", reader.GetDataTypeName(colPostalCode));
                    Assert.AreEqual("int", reader.GetDataTypeName(colInt));
                    Assert.AreEqual("decimal", reader.GetDataTypeName(colDecimal));
                    Assert.AreEqual("float", reader.GetDataTypeName(colFloat));
                    Assert.AreEqual("bit", reader.GetDataTypeName(colBoolean));
                    Assert.AreEqual("datetime", reader.GetDataTypeName(colDateTime));
                    int rowCount = 0;
                    while (reader.Read())
                    {
                        for (int i = 0; i < col; i++)
                        {
                            Assert.IsFalse(reader.IsDBNull(i));
                        }

                        rowCount++;
                        Assert.AreEqual(rowCount, reader.GetInt32(colId));

                        Guid guid = reader.GetGuid(colGuid);
                        Assert.AreNotEqual(Guid.Empty, guid);

                        string guidString = reader.GetString(colGuidString);
                        Guid guidFromString = Guid.Parse(guidString);
                        Assert.AreNotEqual(Guid.Empty, guidFromString);
                        Assert.AreNotEqual(guid, guidFromString);

                        string country = reader.GetString(colCountry);
                        Assert.IsTrue(Countries.ContainsKey(country), $"Country not found: {country}");

                        string region = reader.GetString(colRegion);
                        Assert.IsTrue(Regions.ContainsKey(region), $"Region not found: {region}. (Country: {country})");

                        string city = reader.GetString(colCity);
                        Assert.IsTrue(Cities.ContainsKey(city), $"City not found: {city}. (Country: {country}, Region: {region})");

                        string addressLine1 = reader.GetString(colAddressLine1);
                        Assert.IsFalse(string.IsNullOrWhiteSpace(addressLine1));

                        string addressLine2 = reader.GetString(colAddressLine2);
                        Assert.IsFalse(string.IsNullOrWhiteSpace(addressLine2));

                        string latitudeLongitude = reader.GetString(colLatitudeLongitude);
                        Assert.IsFalse(string.IsNullOrWhiteSpace(latitudeLongitude));

                        string randomText = reader.GetString(colRandomText);
                        Assert.IsFalse(string.IsNullOrWhiteSpace(randomText));

                        string fullName = reader.GetString(colFullName);
                        Assert.AreEqual(1, fullName.Count(c => c == ' '), $"Generated Full name: {fullName}");

                        string email = reader.GetString(colEmail);
                        Assert.IsTrue(IsValidEmail(email), $"Generated Email: {email}");

                        string phone = reader.GetString(colPhone);
                        Regex regex = new Regex(@"[\d\s]+");
                        Assert.IsTrue(regex.IsMatch(phone), $"Generated Phone: {phone}");

                        string passport = reader.GetString(colPassport);
                        regex = new Regex(@"[\d\s]+");
                        Assert.IsTrue(regex.IsMatch(passport), $"Generated Passport: {passport}");

                        string postalCode = reader.GetString(colPostalCode);
                        regex = new Regex(@"[\d]+");
                        Assert.IsTrue(regex.IsMatch(postalCode), $"Generated Postal code: {postalCode}");

                        int intValue = reader.GetInt32(colInt);
                        Assert.IsTrue(intValue >= 0);

                        decimal decimalNumber = reader.GetDecimal(colDecimal);
                        Assert.IsTrue(decimalNumber >= 0);

                        double floatNumber = reader.GetDouble(colFloat);
                        Assert.IsTrue(floatNumber >= 0);

                        bool boolValue = reader.GetBoolean(colBoolean);
                        Assert.IsTrue(boolValue || !boolValue);

                        DateTime dateTime = reader.GetDateTime(colDateTime);
                        Assert.IsTrue(dateTime > DateTime.Now.AddYears(-10));
                        Assert.IsTrue(dateTime < DateTime.Now.AddYears(10));
                    }
                    Assert.AreEqual(generationOptions.RowCount, rowCount);
                }
            }
        }
    }
}
