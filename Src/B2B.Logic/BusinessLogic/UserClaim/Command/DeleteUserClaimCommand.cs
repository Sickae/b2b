using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.UserClaim.Command
{
    public class DeleteUserClaimCommand : DeleteEntityCommandBase
    {
    }

    public class
        DeleteUserClaimCommandHandler : DeleteEntityCommandHandlerBase<UserClaimEntity, DeleteUserClaimCommand>
    {
        public DeleteUserClaimCommandHandler(ISession session, LoggingService loggingService)
            : base(session, loggingService)
        {
        }
    }
}
