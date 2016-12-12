using System;
using System.ComponentModel.DataAnnotations;
using APaers.Common.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APaers.DataGen.Abstract.Generate
{
    [JsonConverter(typeof(ColumnInfoJsonConverter))]
    public abstract class ColumnInfo
    {
        public const char CommonPlaceholder = '_';

        public abstract ColumnType ColumnType { get; }
        [Required]
        public SystemColumnType SystemColumnType { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ColumnId { get; set; }

        private int maxLength;
        public int MaxLength
        {
            get { return maxLength; }
            set { maxLength = value < 0 ? 0 : value; }
        }

        public int MaxPrecision { get; private set; }
        public void SetMaxPrecision(int value)
        {
            MaxPrecision = value;
        }

        private int precision;
        [JsonProperty(Order = 2)]
        public int Precision
        {
            get { return precision; }
            set
            {
                if (precision == value) return;
                if (value < 0)
                    value = 0;
                else if (value > MaxPrecision)
                    value = MaxPrecision;
                precision = value;
                userFormat = null;
            }
        }

        public int Scale { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsComputed { get; set; }

        private byte nullPercent = 33;

        /// <summary> Null values percentage </summary>
        public byte NullPercent
        {
            get { return nullPercent; }
            set { nullPercent = value > 100 ? (byte)100 : value; }
        }

        private string userFormat;
        public virtual string DefaultFormat => null;

        [JsonProperty(Order = 1)]
        public string Format
        {
            get { return userFormat ?? DefaultFormat; }
            set
            {
                if (value != DefaultFormat)
                    userFormat = value;
            }
        }
    }

    internal class ColumnInfoJsonConverter : JsonCreationConverter<ColumnInfo>
    {
        protected override ColumnInfo Create(Type objectType, JObject jObject)
        {
            ColumnType columnType;
            try
            {
                columnType = (ColumnType)jObject.Value<int>("columnType");
            }
            catch
            {
                return null;
            }
            return ColumnInfoHelper.CreateColumnInfo(columnType);
        }
    }

}