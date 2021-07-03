using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Query;
using B2B.Shared.Dto;
using NHibernate;
using NHibernate.Criterion;

namespace B2B.Logic.BusinessLogic.User.Query
{
    public class UserQuery : EntityQueryBase<UserDto>
    {
        public string UserName { get; set; }
        public string InGameName { get; set; }
    }

    public class UserQueryHandler : EntityQueryHandlerBase<UserEntity, UserDto, UserQuery>
    {
        public UserQueryHandler(ISession session, IMapper mapper) : base(session, mapper)
        {
        }

        protected override void SetupWhere(IQueryOver<UserEntity, UserEntity> queryOver, UserQuery request)
        {
            if (!string.IsNullOrEmpty(request.UserName))
                queryOver = queryOver.Where(Restrictions.InsensitiveLike(Projections.Property(() => RootAlias.UserName),
                    request.UserName, MatchMode.Anywhere));

            if (!string.IsNullOrEmpty(request.InGameName))
                queryOver = queryOver.Where(Restrictions.InsensitiveLike(
                    Projections.Property(() => RootAlias.InGameName), request.InGameName, MatchMode.Anywhere));
        }
    }
}
