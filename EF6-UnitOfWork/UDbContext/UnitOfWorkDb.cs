namespace EF6_UnitOfWork.UDbContext
{
	public class UnitOfWorkDb : System.Data.Entity.DbContext, IUnitOfWork
	{
		public UnitOfWorkDb(string nameOrConnectionString) : base(nameOrConnectionString) { }

		public ITransaction BeginTransaction()
		{
			return new DbTransaction(this.Database.BeginTransaction());
		}

		public IQueryRepository<TEntity> QueryRepository<TEntity>() where TEntity : class
		{
			return new QueryRepository<TEntity>(this);
		}
	}
}