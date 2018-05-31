using System.Data.Entity;

namespace EF6_UnitOfWork.UDbContext
{
	public class DbTransaction : ITransaction
	{
		private readonly DbContextTransaction _transaction;

		public DbTransaction(DbContextTransaction transaction)
		{
			_transaction = transaction;
		}

		public void Rollback()
		{
			_transaction.Rollback();
		}

		public void Commit()
		{
			_transaction.Commit();
		}

		public void Dispose()
		{
			_transaction.Dispose();
		}
	}
}