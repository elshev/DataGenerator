export enum SqlType {
  SqlServer = 0,
  MySql = 1
}

export enum ColumnType {
  RandomText = 0,
  FirstName = 1,
  LastName = 2,
  FullName = 3,
  Email = 4,
  Phone = 5,
  PassportNumber = 6,
  PostalCode = 7,
  Country = 8,
  Region = 9,
  City = 10,
  AddressLine1 = 11,
  AddressLine2 = 12,
  FullAddress = 13,
  LatitudeLongitude = 14,
  Int = 15,
  Number = 16,
  Money = 17,
  DateTime = 18,
  Boolean = 19,
  Autoinc = 20,
  Guid = 21
}

export class ColumnTypeInfo {
  columnType: ColumnType;
  stringValue: string;
  displayValue: string;
  category: string;
}

export let ColumnTypeInfos: ColumnTypeInfo[] = [
  { columnType: ColumnType.RandomText, stringValue: "RandomText", displayValue: "Random text", category: "Text" },
  { columnType: ColumnType.FirstName, stringValue: "FirstName", displayValue: "First name", category: "Person" },
  { columnType: ColumnType.LastName, stringValue: "LastName", displayValue: "Last name", category: "Person" },
  { columnType: ColumnType.FullName, stringValue: "FullName", displayValue: "Full name", category: "Person" },
  { columnType: ColumnType.Email, stringValue: "Email", displayValue: "E-Mail", category: "Person" },
  { columnType: ColumnType.Phone, stringValue: "Phone", displayValue: "Phone", category: "Person" },
  { columnType: ColumnType.PassportNumber, stringValue: "PassportNumber", displayValue: "Passport/ID", category: "Person" },
  { columnType: ColumnType.PostalCode, stringValue: "PostalCode", displayValue: "Postal code", category: "Address" },
  { columnType: ColumnType.Country, stringValue: "Country", displayValue: "Country", category: "Address" },
  { columnType: ColumnType.Region, stringValue: "Region", displayValue: "Region", category: "Address" },
  { columnType: ColumnType.City, stringValue: "City", displayValue: "City", category: "Address" },
  { columnType: ColumnType.AddressLine1, stringValue: "AddressLine1", displayValue: "AddressLine1", category: "Address" },
  { columnType: ColumnType.AddressLine2, stringValue: "AddressLine2", displayValue: "AddressLine2", category: "Address" },
  { columnType: ColumnType.FullAddress, stringValue: "FullAddress", displayValue: "Full address", category: "Address" },
  { columnType: ColumnType.LatitudeLongitude, stringValue: "LatitudeLongitude", displayValue: "Latitude/Longitude", category: "Address" },
  { columnType: ColumnType.Int, stringValue: "Int", displayValue: "Integer", category: "Numbers" },
  { columnType: ColumnType.Number, stringValue: "Number", displayValue: "Number", category: "Numbers" },
  { columnType: ColumnType.Money, stringValue: "Money", displayValue: "Money", category: "Numbers" },
  { columnType: ColumnType.DateTime, stringValue: "DateTime", displayValue: "Date/Time", category: "Date/Time" },
  { columnType: ColumnType.Boolean, stringValue: "Boolean", displayValue: "Boolean", category: "Boolean" },
  { columnType: ColumnType.Autoinc, stringValue: "Autoinc", displayValue: "Autoincrement", category: "Identity" },
  { columnType: ColumnType.Guid, stringValue: "Guid", displayValue: "GUID", category: "Identity" }
];

export enum FullNameFormat {
  FirstLast = 0,
  LastFirst = 1,
  FirstMLast = 2,
  LastMFirst = 3,
  FMLast = 4,
  LastFM = 5 }

// fullNameFormatInfo = { "value" = 0, "stringValue" = "FirstLast", "displayValue" = "First Last (James Williams)",
// "category" = null }, { "value" = 1, "stringValue" = "LastFirst", "displayValue" = "Last First (Williams James)",
// "category" = null }, { "value" = 2, "stringValue" = "FirstMLast", "displayValue" = "First M. Last (James M. Williams)",
// "category" = null }, { "value" = 3, "stringValue" = "LastMFirst", "displayValue" = "Last M. First (Williams M. James)",
// "category" = null }, { "value" = 4, "stringValue" = "FMLast", "displayValue" = "F.M. Last (J.M. Williams)", "category" = null },
// { "value" = 5, "stringValue" = "LastFM", "displayValue" = "Last F.M. (Williams J.M.)", "category" = null }

