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
                return ((int)(object)type & (int)(object)value) == (int)(object)value;
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
                return (UserRole)(object)((int)(object)type | (int)(object)value);
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
                return (UserRole)(object)((int)(object)type & ~(int)(object)value);
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
