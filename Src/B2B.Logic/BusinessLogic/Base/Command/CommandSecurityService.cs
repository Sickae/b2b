using System;
using System.Linq;
using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;
using B2B.Shared.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace B2B.Logic.BusinessLogic.Base.Command
{
    public static class CommandSecurityService<TEntity>
        where TEntity : EntityBase
    {
        public static void SetDefaultValues(TEntity entity)
        {
            entity.CreationDateUtc = DateTime.UtcNow;
            entity.ModificationDateUtc = DateTime.UtcNow;

            if (typeof(ILogicalDeletableEntity).IsAssignableFrom(typeof(TEntity)))
                ((ILogicalDeletableEntity) entity).IsDeleted = false;
        }

        public static ICommandResult SecureEntityIntegration(ISession session, TEntity entity)
        {
            var uniqueResult = SecureLogicalUniqueConstraint(session, entity);
            if (!uniqueResult.Success) return uniqueResult;

            return new CommandResult {Success = true};
        }

        private static ICommandResult SecureLogicalUniqueConstraint(ISession session, TEntity entity)
        {
            if (!typeof(ILogicalDeletableEntity).IsAssignableFrom(typeof(TEntity)))
                return new CommandResult {Success = false};

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
