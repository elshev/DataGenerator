using System;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Log
{
    public abstract class LogBase : ILog
    {
        public void Debug(string message)
        {
            Log(LogMessageType.Debug, message);
        }

        public void Info(string message)
        {
            Log(LogMessageType.Info, message);
        }

        public void Warning(string message)
        {
            Log(LogMessageType.Warning, message);
        }

        public void Error(string message)
        {
            Log(LogMessageType.Error, message);
        }

        public void Log(LogMessageType logMessageType, string message)
        {
            LogMessage logMessage = new LogMessage
            {
                Type = (byte) logMessageType,
                DateTime = DateTime.Now,
                Message = message
            };
            Log(logMessage);
        }

        public abstract void Log(LogMessage entity);
    }
}