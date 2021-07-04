using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.User.Command
{
    public class CreateUserCommand : CreateEntityCommandBase<UserDto>
    {
    }

    public class CreateUserCommandHandler : CreateEntityCommandHandlerBase<UserEntity, UserDto, CreateUserCommand>
    {
        public CreateUserCommandHandler(ISession session, IMapper mapper) : base(session, mapper)
        {
        }
    }
}
