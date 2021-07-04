using System;
using System.Reflection;
using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities;
using B2B.Logic.Infrastructure;
using B2B.Shared.Enums;
using B2B.Shared.Interfaces;
using Newtonsoft.Json;
using NHibernate;

namespace B2B.Logic.BusinessLogic.Base.Service
{
    public class LoggingService
    {
        private readonly IAppContext _appContext;
        private readonly ISession _session;

        public LoggingService(ISession session, IAppContext appContext)
        {
            _session = session;
            _appContext = appContext;
        }

        public void LogOperation(IEntity entity, LogOperationType operationType, int? userId = null)
        {
            if (entity != null && entity.GetType().GetCustomAttribute<AuditLogAttribute>() == null)
                return;

            var entityJson = operationType is (LogOperationType.Modify or LogOperationType.Create) && entity != null
                ? JsonConvert.SerializeObject(entity, Formatting.None, new JsonSerializerSettings
                {
                    ContractResolver = new CustomJsonContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })
                : null;

            var logUserId = userId ?? _appContext.UserId;
            var auditLog = new AuditLogEntity
            {
                User = logUserId.HasValue ? _session.Load<UserEntity>(logUserId.Value) : null,
                OperationType = operationType,
                RelatedEntityId = entity?.Id,
                RelatedEntityJson = entityJson,
                CreationDateUtc = DateTime.UtcNow,
                ModificationDateUtc = DateTime.UtcNow
            };

            _session.Save(auditLog);
        }
    }
}
