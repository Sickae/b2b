using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using B2B.DataAccess.Entities.Base;
using B2B.Shared.Interfaces;
using MediatR;
using NHibernate;
using NHibernate.Criterion;

namespace B2B.Logic.BusinessLogic.Base.Query
{
    public abstract class EntityQueryBase<TDto> : IRequest<ICollection<TDto>>
        where TDto : IDto
    {
        public ICollection<int> Ids { get; set; }
    }

    public abstract class
        EntityQueryHandlerBase<TEntity, TDto, TRequest> : RequestHandler<TRequest, ICollection<TDto>>
        where TEntity : EntityBase
        where TDto : IDto
        where TRequest : IRequest<ICollection<TDto>>
    {
        private readonly IMapper _mapper;

        private readonly ISession _session;
        protected TEntity RootAlias = default;

        protected EntityQueryHandlerBase(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        protected override ICollection<TDto> Handle(TRequest request)
        {
            if (request is not EntityQueryBase<TDto> entityRequest)
                throw new InvalidOperationException();

            var queryOver = _session.QueryOver(() => RootAlias);

            if (entityRequest.Ids?.Count > 0)
                queryOver = queryOver.Where(Restrictions.In(Projections.Property(() => RootAlias.Id),
                    entityRequest.Ids.ToArray()));

            if (typeof(ILogicalDeletableEntity).IsAssignableFrom(typeof(TEntity)))
                queryOver = queryOver.Where(x => !((ILogicalDeletableEntity) x).IsDeleted);

            SetupWhere(queryOver, request);

            var list = queryOver.List();
            return _mapper.Map<ICollection<TDto>>(list);
        }

        protected virtual void SetupWhere(IQueryOver<TEntity, TEntity> queryOver, TRequest request)
        {
        }
    }
}
