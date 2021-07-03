using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Query;
using B2B.Shared.Dto;
using NHibernate;
using NHibernate.Criterion;

namespace B2B.Logic.BusinessLogic.UserClaim.Query
{
    public class UserClaimQuery : EntityQueryBase<UserClaimDto>
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public int? UserId { get; set; }
    }

    public class UserClaimQueryHandler : EntityQueryHandlerBase<UserClaimEntity, UserClaimDto, UserClaimQuery>
    {
        public UserClaimQueryHandler(ISession session, IMapper mapper) : base(session, mapper)
        {
        }

        protected override void SetupWhere(IQueryOver<UserClaimEntity, UserClaimEntity> queryOver,
            UserClaimQuery request)
        {
            if (!string.IsNullOrEmpty(request.Type))
                queryOver = queryOver.Where(Restrictions.InsensitiveLike(
                    Projections.Property(() => RootAlias.ClaimType), request.Type, MatchMode.Anywhere));

            if (!string.IsNullOrEmpty(request.Value))
                queryOver = queryOver.Where(Restrictions.InsensitiveLike(
                    Projections.Property(() => RootAlias.ClaimValue), request.Value, MatchMode.Anywhere));

            if (request.UserId.HasValue)
                queryOver = queryOver.Where(x => x.User.Id == request.UserId.Value);
        }
    }
}
