using System;

namespace APaers.DataGen.Abstract.Exceptions
{
    /// <summary>
    /// Business exception that will be processed other than system exceptions
    /// As a rule message will be shown to user.
    /// </summary>
    public class DataGenExceptionBase : Exception
    {
        public const string ReasonPrefix = "DataGen error: ";
        public string Reason { get; set; }
        public string ReasonPhrase => $"{ReasonPrefix}{Reason ?? string.Empty}";

        public DataGenExceptionBase(string message)
            : base(message)
        {
        }

        public DataGenExceptionBase(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}