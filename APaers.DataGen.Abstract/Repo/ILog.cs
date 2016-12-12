namespace APaers.DataGen.Abstract.Repo
{
    public enum LogMessageType
    {
        Debug,
        Info,
        Warning,
        Error
    }

    public interface ILog
    {
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Log(LogMessageType logMessageType, string message);
    }
}