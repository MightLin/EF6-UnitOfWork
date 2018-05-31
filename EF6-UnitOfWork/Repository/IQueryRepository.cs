using System;
using System.Linq;
using System.Linq.Expressions;

namespace EF6_UnitOfWork
{
	public interface IQueryRepository<TEntity> : IDisposable where TEntity : class
	{
		/// <summary>
		/// 取得某一筆資料
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		TEntity Get(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// 取得全部資料來做查詢
		/// </summary>
		/// <returns></returns>
		IQueryable<TEntity> GetAll();

	}
}