using System.ComponentModel;

namespace B2B.Shared.Enums
{
    /// <summary>
    /// Sorted by privileges.
    /// </summary>
    public enum UserRole
    {
        [Description("Admin")]
        Admin,

        [Description("Manager")]
        Manager,

        [Description("Member")]
        Member,

        [Description("Guest")]
        Guest
    }
}
