using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Dto;
using B2B.Shared.Enums;
using NHibernate;

namespace B2B.Logic.BusinessLogic.Application.Command
{
    public class CreateApplicationCommand : CreateEntityCommandBase<ApplicationDto>
    {
    }

    public class CreateApplicationCommandHandler : CreateEntityCommandHandlerBase<ApplicationEntity, ApplicationDto,
        CreateApplicationCommand>
    {
        public CreateApplicationCommandHandler(ISession session, IMapper mapper, LoggingService loggingService)
            : base(session, mapper, loggingService)
        {
        }

        protected override void BeforeSave(ApplicationEntity entity, CreateApplicationCommand request)
        {
            entity.Status = ApplicationStatus.Pending;
        }
    }
}
