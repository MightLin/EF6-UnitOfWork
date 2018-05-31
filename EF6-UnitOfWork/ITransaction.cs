using System;

namespace EF6_UnitOfWork
{
	public interface ITransaction : IDisposable
	{
		void Rollback();

		void Commit();
	}
}