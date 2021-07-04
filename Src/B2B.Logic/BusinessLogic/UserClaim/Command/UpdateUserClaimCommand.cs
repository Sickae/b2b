using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.UserClaim.Command
{
    public class UpdateUserClaimCommand : UpdateEntityCommandBase<UserClaimDto>
    {
    }

    public class
        UpdateUserClaimCommandHandler : UpdateEntityCommandHandlerBase<UserClaimEntity, UserClaimDto,
            UpdateUserClaimCommand>
    {
        public UpdateUserClaimCommandHandler(ISession session, IMapper mapper, LoggingService loggingService)
            : base(session, mapper, loggingService)
        {
        }
    }
}
