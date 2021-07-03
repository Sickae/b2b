using System;
using B2B.Shared.Dto.User;
using MediatR;

namespace B2B.Logic.BusinessLogic.User.Query
{
    public class UserQuery : IRequest<UserDtoBase>
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string InGameName { get; set; }
    }

    // TODO: More abstraction needed.
    public class UserQueryHandler : RequestHandler<UserQuery, UserDtoBase>
    {
        protected override UserDtoBase Handle(UserQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
