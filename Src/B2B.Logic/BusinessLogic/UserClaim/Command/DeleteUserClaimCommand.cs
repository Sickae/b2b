using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.UserClaim.Command
{
    public class DeleteUserClaimCommand : DeleteEntityCommandBase
    {
    }

    public class
        DeleteUserClaimCommandHandler : DeleteEntityCommandHandlerBase<UserClaimEntity, UserClaimDto,
            DeleteUserClaimCommand>
    {
        public DeleteUserClaimCommandHandler(ISession session) : base(session)
        {
        }
    }
}
