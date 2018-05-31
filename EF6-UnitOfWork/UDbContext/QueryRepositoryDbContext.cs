using System;
using System.Linq;
using System.Linq.Expressions;

namespace EF6_UnitOfWork.UDbContext
{
	public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
	{
		protected System.Data.Entity.DbContext Db;

		public QueryRepository(IUnitOfWorkFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException(nameof(factory));
			}
			Db = factory.UnitOfWork as System.Data.Entity.DbContext;
		}

		public QueryRepository(System.Data.Entity.DbContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}
			Db = context;
		}

		public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
		{
			var result = Db.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
			return result;
		}

		public virtual IQueryable<TEntity> GetAll()
		{
			return Db.Set<TEntity>().AsNoTracking().AsQueryable();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (Db != null)
				{
					Db.Dispose();
					Db = null;
				}
			}
		}
	}
}