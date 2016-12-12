using System;
using System.Collections.Generic;
using APaers.DataGen.Abstract.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Interfaces
{
    [TestClass]
    public class ColumnInfoHelperTest: TestBase
    {
        [TestMethod]
        public void TestCreateColumnInfoByColumnType()
        {
            // Arrange
            Dictionary<ColumnType, Type> columnTypeInfoMap = new Dictionary<ColumnType, Type>
            {
                {ColumnType.RandomText, typeof(RandomTextColumnInfo)},
                {ColumnType.FirstName, typeof(FirstNameColumnInfo)},
                {ColumnType.LastName, typeof(LastNameColumnInfo)},
                {ColumnType.FullName, typeof(FullNameColumnInfo)},
                {ColumnType.Email, typeof(EmailColumnInfo)},
                {ColumnType.Phone, typeof(PhoneColumnInfo)},
                {ColumnType.PassportNumber, typeof(PassportNumberColumnInfo)},
                {ColumnType.PostalCode, typeof(PostalCodeColumnInfo)},
                {ColumnType.Country, typeof(CountryColumnInfo)},
                {ColumnType.Region, typeof(RegionColumnInfo)},
                {ColumnType.City, typeof(CityColumnInfo)},
                {ColumnType.AddressLine1, typeof(AddressLine1ColumnInfo)},
                {ColumnType.AddressLine2, typeof(AddressLine2ColumnInfo)},
                {ColumnType.FullAddress, typeof(FullAddressColumnInfo)},
                {ColumnType.LatitudeLongitude, typeof(LatitudeLongitudeColumnInfo)},
                {ColumnType.Int, typeof(IntColumnInfo)},
                {ColumnType.Number, typeof(NumberColumnInfo)},
                {ColumnType.Money, typeof(MoneyColumnInfo)},
                {ColumnType.DateTime, typeof(DateTimeColumnInfo)},
                {ColumnType.Boolean, typeof(BooleanColumnInfo)},
                {ColumnType.Autoinc, typeof(AutoincColumnInfo)},
                {ColumnType.Guid, typeof(GuidColumnInfo)}
            };
        
            foreach (var columnType in columnTypeInfoMap.Keys)
            {
                // Act
                ColumnInfo columnInfo = ColumnInfoHelper.CreateColumnInfo(columnType);
                // Assert
                Assert.IsInstanceOfType(columnInfo, columnTypeInfoMap[columnType]);
                Assert.AreEqual(columnType, columnInfo.ColumnType);
            }
        }

        private static void TestCreateColumnInfo<TColumnInfo>(SystemColumnType systemColumnType, string columnName)
            where TColumnInfo: ColumnInfo
        {
            // Act
            TColumnInfo columnInfo = ColumnInfoHelper.CreateColumnInfo(systemColumnType, columnName) as TColumnInfo;
            // Assert
            Assert.IsNotNull(columnInfo);
            Assert.AreEqual(systemColumnType, columnInfo.SystemColumnType);
            Assert.AreEqual(columnName, columnInfo.Name);
        }

        [TestMethod]
        public void TestCreateColumnInfoBySystemTypeAndColumnName()
        {
            // Arrange
            // Act
            TestCreateColumnInfo<AutoincColumnInfo>(SystemColumnType.Int, "id");
            TestCreateColumnInfo<AutoincColumnInfo>(SystemColumnType.String, "Id");

            TestCreateColumnInfo<GuidColumnInfo>(SystemColumnType.String, "some_guid");
            TestCreateColumnInfo<GuidColumnInfo>(SystemColumnType.Guid, "some_id");

            TestCreateColumnInfo<FirstNameColumnInfo>(SystemColumnType.String, "SomeFirstNameColumn");
            TestCreateColumnInfo<FirstNameColumnInfo>(SystemColumnType.String, "Some_First_Name_Column");
            TestCreateColumnInfo<FirstNameColumnInfo>(SystemColumnType.String, "some_first_Name_column");
            TestCreateColumnInfo<FirstNameColumnInfo>(SystemColumnType.String, "FirstName");
            TestCreateColumnInfo<FirstNameColumnInfo>(SystemColumnType.String, "firstName");
            TestCreateColumnInfo<LastNameColumnInfo>(SystemColumnType.String, "preLastName");
            TestCreateColumnInfo<FullNameColumnInfo>(SystemColumnType.String, "full_name");
            TestCreateColumnInfo<EmailColumnInfo>(SystemColumnType.String, "PersonalEMail");
            TestCreateColumnInfo<PhoneColumnInfo>(SystemColumnType.String, "Telephone");
            TestCreateColumnInfo<PassportNumberColumnInfo>(SystemColumnType.String, "passport_no");
            TestCreateColumnInfo<PassportNumberColumnInfo>(SystemColumnType.String, "passport_no");
            TestCreateColumnInfo<CountryColumnInfo>(SystemColumnType.String, "colCountry");
            TestCreateColumnInfo<RegionColumnInfo>(SystemColumnType.String, "stateName");
            TestCreateColumnInfo<RegionColumnInfo>(SystemColumnType.String, "regionname");
            TestCreateColumnInfo<CityColumnInfo>(SystemColumnType.String, "CityName");
            TestCreateColumnInfo<AddressLine2ColumnInfo>(SystemColumnType.String, "street2Address");
            TestCreateColumnInfo<AddressLine2ColumnInfo>(SystemColumnType.String, "AddressLine2");
            TestCreateColumnInfo<AddressLine1ColumnInfo>(SystemColumnType.String, "street1_address");
            TestCreateColumnInfo<AddressLine1ColumnInfo>(SystemColumnType.String, "_streetAddress");
            TestCreateColumnInfo<AddressLine1ColumnInfo>(SystemColumnType.String, "_address_line_1");
            TestCreateColumnInfo<PostalCodeColumnInfo>(SystemColumnType.String, "my_postalCode");
            TestCreateColumnInfo<PostalCodeColumnInfo>(SystemColumnType.String, "Zip");
            TestCreateColumnInfo<FullAddressColumnInfo>(SystemColumnType.String, "HomeAddress");
            TestCreateColumnInfo<LatitudeLongitudeColumnInfo>(SystemColumnType.String, "latitude");
            TestCreateColumnInfo<LatitudeLongitudeColumnInfo>(SystemColumnType.String, "LongitudeAndLatitude");

            TestCreateColumnInfo<IntColumnInfo>(SystemColumnType.Int, "SomeIntegerColumn");
            TestCreateColumnInfo<IntColumnInfo>(SystemColumnType.Int, "CityId");
            TestCreateColumnInfo<IntColumnInfo>(SystemColumnType.Int, "Country_Id");

            TestCreateColumnInfo<MoneyColumnInfo>(SystemColumnType.Money, "ItemPrice");
            TestCreateColumnInfo<MoneyColumnInfo>(SystemColumnType.String, "ItemPrice");

            TestCreateColumnInfo<DateTimeColumnInfo>(SystemColumnType.DateTime, "create_date");
            TestCreateColumnInfo<DateTimeColumnInfo>(SystemColumnType.DateTime, "createtime");
            TestCreateColumnInfo<DateTimeColumnInfo>(SystemColumnType.DateTime, "UpdateDateTime");
            TestCreateColumnInfo<DateTimeColumnInfo>(SystemColumnType.String, "UpdateDateTime");

            TestCreateColumnInfo<BooleanColumnInfo>(SystemColumnType.Boolean, "flag");
            TestCreateColumnInfo<BooleanColumnInfo>(SystemColumnType.Boolean, "SomeFlag");
            TestCreateColumnInfo<BooleanColumnInfo>(SystemColumnType.String, "SomeFlag");

            TestCreateColumnInfo<RandomTextColumnInfo>(SystemColumnType.String, "SomeField");

            //Assert
        }

    }
}