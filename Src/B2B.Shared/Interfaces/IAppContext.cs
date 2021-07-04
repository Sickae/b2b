using System.Security.Principal;

namespace B2B.Shared.Interfaces
{
    public interface IAppContext
    {
        int? UserId { get; }
        IPrincipal CurrentUser { get; }
    }
}
