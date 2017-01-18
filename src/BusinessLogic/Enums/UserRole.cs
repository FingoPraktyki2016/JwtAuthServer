using System;

namespace LegnicaIT.BusinessLogic.Enums
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        User = 1,
        Manager = 2,
        SuperAdmin = 4
    }
}
