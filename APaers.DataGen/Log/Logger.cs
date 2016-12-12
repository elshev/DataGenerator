using System.Diagnostics;

namespace APaers.DataGen.Log
{
    public class Logger : LogBase
    {
        public override void Log(LogMessage entity)
        {
            Trace.WriteLine($"DataGen Log: {entity.Type} - '{entity.Message}'");
        }
    }
}