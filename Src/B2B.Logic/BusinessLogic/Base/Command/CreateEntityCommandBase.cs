using System;
using AutoMapper;
using B2B.DataAccess.Entities.Base;
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
        protected CreateEntityCommandBase()
        {
        }

        protected CreateEntityCommandBase(TDto dto)
        {
            Dto = dto;
        }

        public TDto Dto { get; set; }
    }

    public abstract class
        CreateEntityCommandHandlerBase<TEntity, TDto, TRequest> : RequestHandler<TRequest, CreateEntityCommandResult>
        where TEntity : EntityBase
        where TDto : IDto
        where TRequest : IRequest<CreateEntityCommandResult>
    {
        private readonly IMapper _mapper;
        private readonly ISession _session;

        protected CreateEntityCommandHandlerBase(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
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
            CommandSecurityService<TEntity>.SetDefaultValues(entity);

            var integrityResult = CommandSecurityService<TEntity>.SecureEntityIntegration(_session, entity);
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

            BeforeSave(entity, request);
            _session.Save(entity);
            AfterSave(entity, request);

            _session.GetCurrentTransaction().Commit();

            return new CreateEntityCommandResult {Success = true};
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
