using System;
using B2B.DataAccess.Entities.Base;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Enums;
using B2B.Shared.Interfaces;
using MediatR;
using NHibernate;

namespace B2B.Logic.BusinessLogic.Base.Command
{
    public class DeleteEntityCommandBase : IRequest<ICommandResult>
    {
        public int Id { get; set; }
    }

    public class DeleteEntityCommandHandlerBase<TEntity, TRequest> : RequestHandler<TRequest, ICommandResult>
        where TEntity : EntityBase
        where TRequest : IRequest<ICommandResult>
    {
        private readonly ISession _session;
        private readonly LoggingService _loggingService;

        protected DeleteEntityCommandHandlerBase(ISession session, LoggingService loggingService)
        {
            _session = session;
            _loggingService = loggingService;
        }

        protected override ICommandResult Handle(TRequest request)
        {
            if (request is not DeleteEntityCommandBase deleteCommand)
                throw new InvalidOperationException();

            var isLogicalEntity = typeof(ILogicalDeletableEntity).IsAssignableFrom(typeof(TEntity));
            var entity = isLogicalEntity
                ? _session.QueryOver<TEntity>().Where(x => !((ILogicalDeletableEntity) x).IsDeleted)
                    .SingleOrDefault()
                : _session.Get<TEntity>(deleteCommand.Id);

            if (entity == null)
                return new CommandResult
                {
                    Success = false,
                    ErrorMessage = $"Entity with id {deleteCommand.Id} was not found."
                };

            BeforeDelete(entity, request);

            entity.ModificationDateUtc = DateTime.UtcNow;

            if (isLogicalEntity)
            {
                ((ILogicalDeletableEntity) entity).IsDeleted = true;
                _session.Merge(entity);
            }
            else
                _session.Delete(deleteCommand.Id);

            _loggingService.LogOperation(entity, LogOperationType.Delete);

            AfterDelete(entity, request);

            return new CommandResult {Success = true};
        }

        protected virtual void BeforeDelete(TEntity entity, TRequest request)
        {
        }

        protected virtual void AfterDelete(TEntity entity, TRequest request)
        {
        }
    }
}
