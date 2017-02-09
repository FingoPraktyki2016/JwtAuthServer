using System;
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Helpers
{
    public static class UserRoleHelpers
    {
        public static bool HasRole(this UserRole type, UserRole value)
        {
            try
            {
                return (type & value) == value;
            }
            catch
            {
                return false;
            }
        }

        public static UserRole AddRole(this UserRole type, UserRole value)
        {
            try
            {
                return (type | value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(UserRole).Name
                        ), ex);
            }
        }

        public static UserRole RemoveRole(this UserRole type, UserRole value)
        {
            try
            {
                return (type & ~value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(UserRole).Name
                        ), ex);
            }
        }
    }
}
