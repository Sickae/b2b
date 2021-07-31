using System;
using System.Linq;
using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;
using B2B.Logic.BusinessLogic.Base.Command;
using B2B.Shared.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace B2B.Logic.BusinessLogic.Base.Service
{
    public static class CommandSecurityService
    {
        public static void SetDefaultValues<TEntity>(TEntity entity)
            where TEntity : EntityBase
        {
            entity.CreationDateUtc = DateTime.UtcNow;
            entity.ModificationDateUtc = DateTime.UtcNow;

            if (typeof(ILogicalDeletableEntity).IsAssignableFrom(typeof(TEntity)))
                ((ILogicalDeletableEntity) entity).IsDeleted = false;
        }

        public static void LoadReferences<TDto, TEntity>(TDto dto, TEntity entity, ISession session)
            where TDto : IDto
            where TEntity : EntityBase
        {
            var referenceProperties = typeof(TEntity).GetProperties()
                .Where(x => x.PropertyType.IsClass && x.PropertyType.Namespace != "System")
                .ToArray();
            var dtoProperties = typeof(TDto).GetProperties()
                .Where(x => x.Name.EndsWith("Id"))
                .ToArray();

            foreach (var prop in referenceProperties)
            {
                var idProp = dtoProperties.FirstOrDefault(x => x.Name == $"{prop.Name}Id");
                var id = idProp?.GetValue(dto);
                if (id == null) continue;
                var loadedEntity = session.Load(prop.PropertyType.Name, id);

                if (loadedEntity != null)
                    prop.SetValue(entity, loadedEntity);
            }
        }

        public static ICommandResult SecureEntityIntegration<TEntity>(ISession session, TEntity entity)
            where TEntity : EntityBase
        {
            var uniqueResult = SecureLogicalUniqueConstraint(session, entity);
            if (!uniqueResult.Success) return uniqueResult;

            return new CommandResult {Success = true};
        }

        private static ICommandResult SecureLogicalUniqueConstraint<TEntity>(ISession session, TEntity entity)
            where TEntity : EntityBase
        {
            if (!typeof(ILogicalDeletableEntity).IsAssignableFrom(typeof(TEntity)))
                return new CommandResult {Success = true};

            var uniqueProperties = entity.GetType().GetProperties()
                .Where(x => x.IsDefined(typeof(UniqueAttribute), false));

            foreach (var prop in uniqueProperties)
            {
                var key = prop.GetValue(entity);
                var existingEntities = session.QueryOver<TEntity>()
                    .Where(Restrictions.Eq(Projections.Property(prop.Name), key))
                    .Where(x => !((ILogicalDeletableEntity) x).IsDeleted && x.Id != entity.Id)
                    .List();

                if (existingEntities is {Count: > 0})
                    return new CommandResult
                    {
                        Success = false,
                        ErrorMessage = $"Logical unique constraint has been violated on property {prop.Name}!"
                    };
            }

            return new CommandResult {Success = true};
        }
    }
}
