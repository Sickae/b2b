using System;
using AutoMapper;
using B2B.DataAccess.Entities.Base;
using B2B.Shared.Interfaces;
using MediatR;
using NHibernate;

namespace B2B.Logic.BusinessLogic.Base.Command
{
    public abstract class UpdateEntityCommandBase<TDto> : IRequest<ICommandResult>
        where TDto : IDto
    {
        public TDto Dto { get; set; }
    }

    public abstract class
        UpdateEntityCommandHandlerBase<TEntity, TDto, TRequest> : RequestHandler<TRequest, ICommandResult>
        where TEntity : EntityBase
        where TDto : IDto
        where TRequest : IRequest<ICommandResult>
    {
        private readonly IMapper _mapper;
        private readonly ISession _session;

        protected UpdateEntityCommandHandlerBase(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        protected override ICommandResult Handle(TRequest request)
        {
            if (request is not UpdateEntityCommandBase<TDto> updateCommand)
                throw new InvalidOperationException();

            if (updateCommand.Dto == null)
                throw new InvalidOperationException($"{nameof(updateCommand.Dto)} cannot be null.");

            var entity = typeof(ILogicalDeletableEntity).IsAssignableFrom(typeof(TEntity))
                ? _session.QueryOver<TEntity>().Where(x => !((ILogicalDeletableEntity) x).IsDeleted)
                    .SingleOrDefault()
                : _session.Get<TEntity>(updateCommand.Dto.Id);

            if (entity == null)
                return new CommandResult
                {
                    Success = false,
                    ErrorMessage = $"Entity with id {updateCommand.Dto.Id} cannot be found."
                };

            entity = MapToEntity(updateCommand.Dto, entity);

            var integrityResult = CommandSecurityService<TEntity>.SecureEntityIntegration(_session, entity);
            if (!integrityResult.Success) return integrityResult;

            var securityResult = SetupSecurityCheck(entity, request);
            if (!securityResult.Success) return securityResult;

            BeforeSave(entity, request);

            entity.ModificationDateUtc = DateTime.UtcNow;
            _session.Merge(entity);

            AfterSave(entity, request);

            return new CommandResult {Success = true};
        }

        protected virtual TEntity MapToEntity(TDto dto, TEntity entity)
        {
            _mapper.Map(dto, entity);
            return entity;
        }

        protected virtual void BeforeSave(TEntity entity, TRequest request)
        {
        }

        protected virtual void AfterSave(TEntity entity, TRequest request)
        {
        }

        protected virtual ICommandResult SetupSecurityCheck(TEntity entity, TRequest request)
        {
            return new CommandResult {Success = true};
        }
    }
}
