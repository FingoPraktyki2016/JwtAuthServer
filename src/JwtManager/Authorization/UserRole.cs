using System;

namespace LegnicaIT.JwtManager.Authorization
{
    [Flags]
    public enum UserRole
    {
        User = 1,
        Manager = 2,
        SuperAdmin = 4
    }
}
