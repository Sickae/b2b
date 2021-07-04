using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.User.Command
{
    public class DeleteUserCommand : DeleteEntityCommandBase
    {
    }

    public class DeleteUserCommandHandler : DeleteEntityCommandHandlerBase<UserEntity, DeleteUserCommand>
    {
        public DeleteUserCommandHandler(ISession session, LoggingService loggingService)
            : base(session, loggingService)
        {
        }
    }
}
