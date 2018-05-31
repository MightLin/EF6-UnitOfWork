using System;
using System.Linq.Expressions;

namespace EF6_UnitOfWork
{
	public interface IRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
	{
		TEntity Create(TEntity instance);

		//int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateFactory);

		/// <summary>
		/// 依照  PrimaryKey 更新欄位
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="keyValues">The key values.</param>
		/// <param name="property">The property.</param>
		/// <param name="value">The value.</param>
		void Update<TProperty>(object[] keyValues, Expression<Func<TEntity, TProperty>> property, TProperty value);

		/// <summary>
		/// 依MODEL的值更新
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="keyValues"></param>
		void Update(TEntity instance, params object[] keyValues);


		/// <summary>
		/// Deletes the specified instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="keyValues">The key values.</param>
		/// <returns></returns>
		void Delete(TEntity instance, params object[] keyValues);

	}
}