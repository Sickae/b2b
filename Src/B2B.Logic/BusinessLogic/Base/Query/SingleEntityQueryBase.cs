using AutoMapper;
using B2B.Shared.Interfaces;
using MediatR;
using NHibernate;

namespace B2B.Logic.BusinessLogic.Base.Query
{
    public abstract class SingleEntityQueryBase<TEntity, TDto> : IRequest<TDto>
        where TEntity : IEntity
        where TDto : IDto
    {
        protected SingleEntityQueryBase()
        {
        }

        protected SingleEntityQueryBase(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public abstract class
        SingleEntityQueryHandlerBase<TEntity, TDto> : RequestHandler<SingleEntityQueryBase<TEntity, TDto>, TDto>
        where TEntity : IEntity
        where TDto : IDto
    {
        private readonly IMapper _mapper;
        private readonly ISession _session;

        protected SingleEntityQueryHandlerBase(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        protected override TDto Handle(SingleEntityQueryBase<TEntity, TDto> request)
        {
            var entity = _session.Get<TEntity>(request.Id);
            return _mapper.Map<TDto>(entity);
        }
    }
}
