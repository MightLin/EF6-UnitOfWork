using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EF6_UnitOfWork
{
	public class GenericRepository<TEntity> : QueryRepository<TEntity>, IRepository<TEntity> where TEntity : class
	{
		public GenericRepository(IUnitOfWorkFactory factory) : base(factory)
		{

		}

		public virtual TEntity Create(TEntity instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));
			return Db.Set<TEntity>().Add(instance);
		}

		public virtual void Update(TEntity instance, params object[] keyValues)
		{
			Update<TEntity>(instance, keyValues);
		}

		public virtual void Update<TProperty>(object[] keyValues, Expression<Func<TEntity, TProperty>> property,
			TProperty value)
		{
			if (keyValues == null || !keyValues.Any())
			{
				throw new ArgumentNullException(nameof(keyValues));
			}
			TEntity instance = Db.Set<TEntity>().Find(keyValues);
			Db.Entry(instance).Property(property).CurrentValue = value;
		}

		public virtual void Delete(TEntity instance, params object[] keyValues)
		{
			Delete<TEntity>(instance, keyValues);
		}

		protected T Update<T>(T instance, params object[] keyValues) where T : class
		{
			if (instance == null)
			{
				throw new ArgumentNullException(nameof(instance));
			}

			if (keyValues == null || !keyValues.Any())
			{
				Db.Entry(instance).State = EntityState.Modified;
				return instance;
			}
			var entry = Db.Entry(instance);
			if (entry.State == EntityState.Detached)
			{
				var set = Db.Set<T>();
				T attachedEntity = set.Find(keyValues);
				if (attachedEntity != null)
				{
					var attachedEntry = Db.Entry(attachedEntity);
					attachedEntry.CurrentValues.SetValues(instance);
					return attachedEntity;
				}
			}
			entry.State = EntityState.Modified;
			return instance;
		}

		protected T Delete<T>(T instance, params object[] keyValues) where T : class
		{
			if (instance == null)
			{
				throw new ArgumentNullException(nameof(instance));
			}
			if (keyValues == null || !keyValues.Any())
			{
				Db.Entry(instance).State = EntityState.Deleted;
			}
			var entry = Db.Entry(instance);
			if (entry.State == EntityState.Detached)
			{
				var attachedEntity = Db.Set<T>().Find(keyValues);
				if (attachedEntity != null)
				{
					Db.Entry(attachedEntity).State = EntityState.Deleted;
					return attachedEntity;
				}
			}
			entry.State = EntityState.Deleted;
			return instance;
		}
	}
}