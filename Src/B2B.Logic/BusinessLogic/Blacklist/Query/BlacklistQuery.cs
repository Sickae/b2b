using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Query;
using B2B.Shared.Dto;
using NHibernate;
using NHibernate.Criterion;

namespace B2B.Logic.BusinessLogic.Blacklist.Query
{
    public class BlacklistQuery : SingleEntityQueryBase<BlacklistDto>
    {
        public string InGameName { get; set; }
    }

    public class BlacklistQueryHandler : SingleEntityQueryHandlerBase<BlacklistEntity, BlacklistDto, BlacklistQuery>
    {
        public BlacklistQueryHandler(ISession session, IMapper mapper) : base(session, mapper)
        {
        }

        protected override Junction SetupWhere(BlacklistQuery request)
        {
            var conjunction = new Conjunction();
            conjunction.Add(Restrictions.Where(() => RootAlias.InGameName == request.InGameName));
            return conjunction;
        }
    }
}
