using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace APaers.DataGen.Abstract.Generate
{
    public static class ColumnInfoHelper
    {
        public class ColumnTypeDesc
        {
            public ColumnType ColumnType { get; }
            public IEnumerable<SystemColumnType> SystemColumnTypes { get; set; } = new List<SystemColumnType>();
            public IEnumerable<string> NamePatterns { get; }

            public ColumnTypeDesc(ColumnType columnType, IEnumerable<string> namePatterns)
            {
                ColumnType = columnType;
                NamePatterns = namePatterns;
            }
        }

        private static List<ColumnTypeDesc> ColumnTypeDescs { get; } = new List<ColumnTypeDesc>
        {
            new ColumnTypeDesc(ColumnType.Autoinc, new []{ "(?i)^id$"}) {SystemColumnTypes = new []{SystemColumnType.Int}},

            new ColumnTypeDesc(ColumnType.Int, new []{ "(?i)integer", @".+_id", @".+Id"}) {SystemColumnTypes = new []{SystemColumnType.Int}},

            new ColumnTypeDesc(ColumnType.FirstName, new []{ "(?i)firstname", "(?i)first_name" }), 
            new ColumnTypeDesc(ColumnType.LastName, new []{ "(?i)lastname", "(?i)last_name"}), 
            new ColumnTypeDesc(ColumnType.FullName, new []{ "(?i)fullname", "(?i)full_name"}), 
            new ColumnTypeDesc(ColumnType.Email, new []{ "(?i)email", "(?i)e_mail"}), 
            new ColumnTypeDesc(ColumnType.Phone, new []{ "(?i)phone"}),
            new ColumnTypeDesc(ColumnType.PassportNumber, new []{ "(?i)passport"}),
            new ColumnTypeDesc(ColumnType.Country, new []{ "(?i)country"}),
            new ColumnTypeDesc(ColumnType.Region, new []{ "(?i)state", "(?i)region"}),
            new ColumnTypeDesc(ColumnType.City, new []{ "(?i)city"}),
            new ColumnTypeDesc(ColumnType.AddressLine2, new []{ "(?i)street2", "(?i)address.?line.?2"}),
            new ColumnTypeDesc(ColumnType.AddressLine1, new []{ "(?i)street1", "(?i)street", "(?i)address.?line.?1"}),
            new ColumnTypeDesc(ColumnType.PostalCode, new []{ "(?i)postal", "(?i)zip"}),
            new ColumnTypeDesc(ColumnType.FullAddress, new []{ "(?i)address"}),
            new ColumnTypeDesc(ColumnType.LatitudeLongitude, new []{ "(?i)latitude", "(?i)longitude"}),
            new ColumnTypeDesc(ColumnType.Int, new []{ "(?i)integer", @"_id\z", @"Id\z"}) {SystemColumnTypes = new []{SystemColumnType.Int}},
            new ColumnTypeDesc(ColumnType.Money, new []{ "(?i)price"}) {SystemColumnTypes = new []{SystemColumnType.Money, SystemColumnType.Number}},
            new ColumnTypeDesc(ColumnType.DateTime, new []{ "(?i)datetime", "(?i)date", "(?i)time"}) {SystemColumnTypes = new []{SystemColumnType.DateTime}},
            new ColumnTypeDesc(ColumnType.Guid, new []{ "(?i)guid" }),
            new ColumnTypeDesc(ColumnType.Boolean, new []{ "(?i)flag" }) {SystemColumnTypes = new []{SystemColumnType.Boolean}},
        };

        private static ColumnType GetColumnType(SystemColumnType systemColumnType, string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentNullException(nameof(columnName));

            foreach (ColumnTypeDesc desc in ColumnTypeDescs)
            {
                if (systemColumnType != SystemColumnType.String && !desc.SystemColumnTypes.Contains(systemColumnType))
                    continue;
                foreach (string namePattern in desc.NamePatterns)
                {
                    Regex regex = new Regex(namePattern);
                    if (regex.IsMatch(columnName))
                        return desc.ColumnType;
                }
            }
            switch (systemColumnType)
            {
                case SystemColumnType.Int: 
                    return ColumnType.Int;
                case SystemColumnType.DateTime: 
                    return ColumnType.DateTime;
                case SystemColumnType.Boolean: 
                    return ColumnType.Boolean;
                case SystemColumnType.Money: 
                    return ColumnType.Money;
                case SystemColumnType.Number:
                    return ColumnType.Number;
                case SystemColumnType.Guid:
                    return ColumnType.Guid;
                default:
                    return ColumnType.RandomText;
            }
        }

        private static Dictionary<ColumnType, Type> ColumnTypeMap { get; } = new Dictionary<ColumnType, Type>();

        public static ColumnInfo CreateColumnInfo(ColumnType columnType)
        {
            if (!ColumnTypeMap.ContainsKey(columnType))
            {
                string assemblyName = typeof(ColumnInfo).Namespace;
                string columnInfoTypeName = $"{assemblyName}.{columnType}ColumnInfo";
                Type columnInfoType = Type.GetType(columnInfoTypeName);
                if (columnInfoType == null)
                    throw new ArgumentException($"Can't find type '{columnInfoTypeName}' for '{columnType}'");
                ColumnTypeMap.Add(columnType, columnInfoType);
            }
            return (ColumnInfo)Activator.CreateInstance(ColumnTypeMap[columnType]);
        }

        public static ColumnInfo CreateColumnInfo(SystemColumnType systemColumnType, string columnName)
        {
            ColumnType columnType = GetColumnType(systemColumnType, columnName);
            ColumnInfo result = CreateColumnInfo(columnType);
            result.Name = columnName;
            result.SystemColumnType = systemColumnType;
            return result;
        }
    }
}