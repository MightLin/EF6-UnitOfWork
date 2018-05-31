using System;
using System.Collections.Generic;

namespace EF6_UnitOfWork
{
	public static class EnumerableExtender
	{
		/// <summary>
		///     使用LAMBA表示式來對IEnumerable介面實作取唯一
		/// </summary>
		/// <typeparam name="TSource">source 之項目的型別</typeparam>
		/// <typeparam name="TKey">keySelector 所傳回之索引鍵的型別</typeparam>
		/// <param name="source">要取唯一的IEnumerable目標集合</param>
		/// <param name="keySelector">用來擷取各項目之索引鍵的函式</param>
		/// <returns></returns>
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			var seenKeys = new HashSet<TKey>();
			foreach (var element in source)
			{
				var elementValue = keySelector(element);
				if (seenKeys.Add(elementValue))
				{
					yield return element;
				}
			}
		}
	}
}