using System;
using AutoMapper;
using B2B.DataAccess.Entities.Base;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Enums;
using B2B.Shared.Interfaces;
using MediatR;
using NHibernate;

namespace B2B.Logic.BusinessLogic.Base.Command
{
    public class CreateEntityCommandResult : CommandResult
    {
        public int NewEntityId { get; init; }
    }

    public abstract class CreateEntityCommandBase<TDto> : IRequest<CreateEntityCommandResult>
        where TDto : IDto
    {
        public TDto Dto { get; set; }
    }

    public abstract class
        CreateEntityCommandHandlerBase<TEntity, TDto, TRequest> : RequestHandler<TRequest, CreateEntityCommandResult>
        where TEntity : EntityBase
        where TDto : IDto
        where TRequest : IRequest<CreateEntityCommandResult>
    {
        private readonly IMapper _mapper;
        private readonly LoggingService _loggingService;
        private readonly ISession _session;

        protected CreateEntityCommandHandlerBase(ISession session, IMapper mapper, LoggingService loggingService)
        {
            _session = session;
            _mapper = mapper;
            _loggingService = loggingService;
        }

        protected override CreateEntityCommandResult Handle(TRequest request)
        {
            if (request is not CreateEntityCommandBase<TDto> createCommand)
                throw new InvalidOperationException();

            if (createCommand.Dto == null)
                throw new InvalidOperationException($"{nameof(createCommand.Dto)} cannot be null.");

            if (createCommand.Dto.Id > 0)
                throw new InvalidCastException($"{nameof(createCommand.Dto.Id)} must be set to 0.");

            var entity = _mapper.Map<TEntity>(createCommand.Dto);
            CommandSecurityService.SetDefaultValues(entity);

            var integrityResult = CommandSecurityService.SecureEntityIntegration(_session, entity);
            if (!integrityResult.Success)
                return new CreateEntityCommandResult
                {
                    Success = false,
                    ErrorMessage = integrityResult.ErrorMessage
                };

            var securityResult = SetupSecurityCheck(entity, request);
            if (!securityResult.Success)
                return new CreateEntityCommandResult
                {
                    Success = false,
                    ErrorMessage = securityResult.ErrorMessage
                };

            CommandSecurityService.LoadReferences(createCommand.Dto, entity, _session);

            BeforeSave(entity, request);

            _session.Save(entity);

            _loggingService.LogOperation(entity, LogOperationType.Create);

            AfterSave(entity, request);

            return new CreateEntityCommandResult
            {
                Success = true,
                NewEntityId = entity.Id
            };
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
