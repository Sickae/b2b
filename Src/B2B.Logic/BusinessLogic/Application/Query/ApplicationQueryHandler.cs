using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Query;
using B2B.Shared.Dto;
using B2B.Shared.Enums;
using NHibernate;
using NHibernate.Criterion;

namespace B2B.Logic.BusinessLogic.Application.Query
{
    public class ApplicationQuery : SingleEntityQueryBase<ApplicationDto>
    {
        public string InGameName { get; set; }
        public ApplicationStatus? Status { get; set; }
    }

    public class ApplicationQueryHandler
        : SingleEntityQueryHandlerBase<ApplicationEntity,ApplicationDto, ApplicationQuery>
    {
        public ApplicationQueryHandler(ISession session, IMapper mapper) : base(session, mapper)
        {
        }

        protected override Junction SetupWhere(ApplicationQuery request)
        {
            var junction = new Conjunction();
            if (!string.IsNullOrEmpty(request.InGameName))
                junction.Add(Restrictions.Where(() => RootAlias.InGameName == request.InGameName));

            if (request.Status.HasValue)
                junction.Add(Restrictions.Where(() => RootAlias.Status == request.Status.Value));

            return junction;
        }
    }
}
