using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using APaers.Common.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace APaers.DataGen.WebApi.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString EnumAsJavascriptObject<T>(this HtmlHelper helper)
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
                throw new ArgumentException($"'{enumType}' is not enum type");

            StringBuilder jsEnum = new StringBuilder();
            string enumName = enumType.Name.ToLowerFirstChar();
            jsEnum.Append($"var {enumName}Enum = Object.freeze({{ ");
            bool isFirst = true;
            foreach (object enumValue in enumType.GetEnumValues())
            {
                int intValue = (int) enumValue;
                string comma = isFirst ? string.Empty : ", ";
                string s = $"{comma}{enumValue}: {intValue}";
                jsEnum.Append(s);
                isFirst = false;
            }
            jsEnum.Append(" });");

            IEnumerable<EnumInfo> enumInfo = EnumHelper.GetEnumInfo(enumType);
            if (enumInfo.Any())
            {
                var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                jsEnum.AppendLine();
                string jsEnumInfo = $"var {enumName}EnumInfo = " + JsonConvert.SerializeObject(enumInfo, jsonSerializerSettings);
                jsEnum.Append(jsEnumInfo);
            }

            return new MvcHtmlString(jsEnum.ToString());
        }
    }
}