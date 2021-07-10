using System;
using AutoMapper;
using B2B.Shared.Interfaces;
using MediatR;
using NHibernate;

namespace B2B.Logic.BusinessLogic.Base.Query
{
    public abstract class SingleEntityQueryBase<TDto> : IRequest<TDto>
        where TDto : IDto
    {
        public virtual int Id { get; set; }
    }

    public abstract class
        SingleEntityQueryHandlerBase<TEntity, TDto, TRequest> : RequestHandler<TRequest, TDto>
        where TEntity : IEntity
        where TDto : IDto
        where TRequest : IRequest<TDto>
    {
        private readonly IMapper _mapper;
        private readonly ISession _session;

        protected SingleEntityQueryHandlerBase(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        protected override TDto Handle(TRequest request)
        {
            if (request is not SingleEntityQueryBase<TDto> singleRequest)
                throw new InvalidOperationException();

            var entity = _session.Get<TEntity>(singleRequest.Id);
            return _mapper.Map<TDto>(entity);
        }
    }
}
