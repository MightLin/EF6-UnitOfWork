namespace EF6_UnitOfWork
{
	public interface IUnitOfWork
	{
		ITransaction BeginTransaction();

		IQueryRepository<TEntity> QueryRepository<TEntity>() where TEntity : class;


		int SaveChanges();
	}
}