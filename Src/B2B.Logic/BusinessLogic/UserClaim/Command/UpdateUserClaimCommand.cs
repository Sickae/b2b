using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.UserClaim.Command
{
    public class UpdateUserClaimCommand : UpdateEntityCommandBase<UserClaimDto>
    {
        public UpdateUserClaimCommand(UserClaimDto dto) : base(dto)
        {
        }
    }

    public class
        UpdateUserClaimCommandHandler : UpdateEntityCommandHandlerBase<UserClaimEntity, UserClaimDto,
            UpdateUserClaimCommand>
    {
        public UpdateUserClaimCommandHandler(ISession session, IMapper mapper) : base(session, mapper)
        {
        }
    }
}
