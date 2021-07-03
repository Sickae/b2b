using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Query;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.User.Query
{
    public class UserByIdQuery : SingleEntityQueryBase<UserEntity, UserDto>
    {
    }

    public class UserByIdQueryHandler : SingleEntityQueryHandlerBase<UserEntity, UserDto>
    {
        public UserByIdQueryHandler(ISession session, IMapper mapper) : base(session, mapper)
        {
        }
    }
}
