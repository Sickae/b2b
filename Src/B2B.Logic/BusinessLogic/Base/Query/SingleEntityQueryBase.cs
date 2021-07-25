using System;
using AutoMapper;
using B2B.DataAccess.Entities.Base;
using B2B.Shared.Interfaces;
using MediatR;
using NHibernate;
using NHibernate.Criterion;

namespace B2B.Logic.BusinessLogic.Base.Query
{
    public abstract class SingleEntityQueryBase<TDto> : IRequest<TDto>
        where TDto : IDto
    {
        public virtual int Id { get; set; }
    }

    public abstract class
        SingleEntityQueryHandlerBase<TEntity, TDto, TRequest> : RequestHandler<TRequest, TDto>
        where TEntity : EntityBase
        where TDto : IDto
        where TRequest : IRequest<TDto>
    {
        protected readonly IMapper Mapper;
        protected readonly ISession Session;
        protected TEntity RootAlias = default;

        protected SingleEntityQueryHandlerBase(ISession session, IMapper mapper)
        {
            Session = session;
            Mapper = mapper;
        }

        protected override TDto Handle(TRequest request)
        {
            if (request is not SingleEntityQueryBase<TDto> singleRequest)
                throw new InvalidOperationException();

            var query = Session.QueryOver(() => RootAlias);
            var junction = SetupWhere(request);

            if (junction?.GetProjections()?.Length > 0)
                query = query.Where(junction);

            if (singleRequest.Id > 0)
                query = query.Where(x => x.Id == singleRequest.Id);

            var entity = query.SingleOrDefault();
            return Mapper.Map<TDto>(entity);
        }

        protected virtual Junction SetupWhere(TRequest request)
        {
            return null;
        }
    }
}
