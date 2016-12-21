namespace LegnicaIT.JwtAuthServer.Interfaces
{
    internal interface IJwtLogger
    {
        void Info(string message);

        void Debug(string message);

        void Warning(string message);

        void Error(string message);
    }
}
