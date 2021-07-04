using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.User.Command
{
    public class UpdateUserCommand : UpdateEntityCommandBase<UserDto>
    {
    }

    public class UpdateUserCommandHandler : UpdateEntityCommandHandlerBase<UserEntity, UserDto, UpdateUserCommand>
    {
        public UpdateUserCommandHandler(ISession session, IMapper mapper, LoggingService loggingService)
            : base(session, mapper, loggingService)
        {
        }
    }
}
