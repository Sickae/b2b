using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Query;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.ApplicationFlow.Query
{
    public class StaticApplicationFlowQuery : SingleEntityQueryBase<ApplicationFlowDto>
    {
        public override int Id => 1;
    }

    public class StaticApplicationFlowQueryHandler : SingleEntityQueryHandlerBase<ApplicationFlowEntity, ApplicationFlowDto,
        StaticApplicationFlowQuery>
    {
        public StaticApplicationFlowQueryHandler(ISession session, IMapper mapper) : base(session, mapper)
        {
        }
    }
}
