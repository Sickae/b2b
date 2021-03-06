﻿using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Dto;
using NHibernate;

namespace B2B.Logic.BusinessLogic.UserClaim.Command
{
    public class CreateUserClaimCommand : CreateEntityCommandBase<UserClaimDto>
    {
    }

    public class
        CreateUserClaimCommandHandler : CreateEntityCommandHandlerBase<UserClaimEntity, UserClaimDto,
            CreateUserClaimCommand>
    {
        public CreateUserClaimCommandHandler(ISession session, IMapper mapper, LoggingService loggingService)
            : base(session, mapper, loggingService)
        {
        }
    }
}
