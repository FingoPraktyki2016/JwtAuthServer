namespace LegnicaIT.JwtAuthServer.Interfaces
{
    public interface IJwtLogger
    {
        void Information(string message);

        void Debug(string message);

        void Warning(string message);

        void Error(string message);

        void Trace(string message);

        void Critical(string message);
    }
}