using APaers.Common.Helpers;

namespace APaers.DataGen.Abstract.Generate
{
    public enum SystemColumnType
    {
        None,
        String,
        Int,
        Number,
        Money,
        DateTime,
        Boolean,
        Guid
    }

    public enum ColumnType
    {
        [EnumInfo("Random text", "Text")]
        RandomText,
        [EnumInfo("First name", "Person")]
        FirstName,
        [EnumInfo("Last name", "Person")]
        LastName,
        [EnumInfo("Full name", "Person")]
        FullName,
        [EnumInfo("E-Mail", "Person")]
        Email,
        [EnumInfo("Phone", "Person")]
        Phone,
        [EnumInfo("Passport/ID", "Person")]
        PassportNumber,
        [EnumInfo("Postal code", "Address")]
        PostalCode,
        [EnumInfo("Country", "Address")]
        Country,
        [EnumInfo("Region", "Address")]
        Region,
        [EnumInfo("City", "Address")]
        City,
        [EnumInfo("AddressLine1", "Address")]
        AddressLine1,
        [EnumInfo("AddressLine2", "Address")]
        AddressLine2,
        [EnumInfo("Full address", "Address")]
        FullAddress,
        [EnumInfo("Latitude/Longitude", "Address")]
        LatitudeLongitude,
        [EnumInfo("Integer", "Numbers")]
        Int,
        [EnumInfo("Number", "Numbers")]
        Number,
        [EnumInfo("Money", "Numbers")]
        Money,
        [EnumInfo("Date/Time", "Date/Time")]
        DateTime,
        [EnumInfo("Boolean", "Boolean")]
        Boolean,
        [EnumInfo("Autoincrement", "Identity")]
        Autoinc,
        [EnumInfo("GUID", "Identity")]
        Guid,
        
        /*,
        [ColumnType("Other", "Xml")]
        Xml*/
    }
}