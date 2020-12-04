using B2B.Shared.Dto.User;
using MediatR;
using System;

namespace B2B.Logic.BusinessLogic.User.Query
{
    public class UserQuery : IRequest<UserDto>
    {
        public int? Id { get; set; }
    }

    // TODO: More abstraction needed.
    public class UserQueryHandler : RequestHandler<UserQuery, UserDto>
    {
        protected override UserDto Handle(UserQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
