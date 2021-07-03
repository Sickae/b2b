using System;
using B2B.DataAccess.Entities.Base;
using B2B.Shared.Interfaces;
using MediatR;
using NHibernate;

namespace B2B.Logic.BusinessLogic.Base.Command
{
    public class DeleteEntityCommandBase : IRequest<ICommandResult>
    {
        public int Id { get; set; }
    }

    public class DeleteEntityCommandHandlerBase<TEntity, TDto, TRequest> : RequestHandler<TRequest, ICommandResult>
        where TEntity : EntityBase
        where TDto : IDto
        where TRequest : IRequest<ICommandResult>
    {
        private readonly ISession _session;

        public DeleteEntityCommandHandlerBase(ISession session)
        {
            _session = session;
        }

        protected override ICommandResult Handle(TRequest request)
        {
            if (request is not DeleteEntityCommandBase deleteCommand)
                throw new InvalidOperationException();

            var entity = _session.Get<TEntity>(deleteCommand.Id);
            if (entity == null)
                return new CommandResult
                {
                    Success = false,
                    ErrorMessage = $"Entity with id {deleteCommand.Id} was not found."
                };

            BeforeDelete(entity, request);
            _session.Delete(deleteCommand.Id);
            AfterDelete(entity, request);

            _session.GetCurrentTransaction().Commit();
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
