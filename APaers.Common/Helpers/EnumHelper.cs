using System;
using System.Collections.Generic;
using System.Reflection;

namespace APaers.Common.Helpers
{
    public class EnumInfoAttribute : Attribute
    {
        public string Category { get; set; }
        public string DisplayValue { get; set; }

        public EnumInfoAttribute(string displayValue, string category)
        {
            DisplayValue = displayValue;
            Category = category;
        }

        public EnumInfoAttribute(string displayValue)
            :this(displayValue, null)
        {
        }
    }

    public class EnumInfo
    {
        public int Value { get; set; }
        public string StringValue { get; set; }
        public string DisplayValue { get; set; }
        public string Category { get; set; }
    }

    public static class EnumHelper
    {
        public static IEnumerable<EnumInfo> GetEnumInfo(Type enumType)
        {
            List<EnumInfo> result = new List<EnumInfo>();
            foreach (object enumValue in Enum.GetValues(enumType))
            {
                MemberInfo[] members = enumType.GetMember(enumValue.ToString());
                if (members.Length == 1)
                {
                    object[] attrs = members[0].GetCustomAttributes(typeof(EnumInfoAttribute), false);
                    if (attrs.Length != 1) continue;
                    EnumInfoAttribute attr = (EnumInfoAttribute)attrs[0];
                    result.Add(new EnumInfo
                    {
                        Value = (int) enumValue,
                        StringValue = enumValue.ToString(),
                        Category = attr.Category,
                        DisplayValue = attr.DisplayValue
                    });
                }
            }
            return result;
        }
    }
}