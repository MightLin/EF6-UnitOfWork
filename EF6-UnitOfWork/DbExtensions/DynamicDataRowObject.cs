using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace EF6_UnitOfWork
{
	/// <summary>
	/// 將 DataRow 轉換為 Dynamic 以物件導向使用
	/// </summary>
	/// <seealso cref="System.Dynamic.DynamicObject" />
	public class DynamicDataRowObject : DynamicObject
	{
		private DataRow _dr;
		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicDataRowObject"/> class.
		/// </summary>
		/// <param name="dr">The dr.</param>
		public DynamicDataRowObject(DataRow dr)
		{
			_dr = dr;
		}

		/// <summary>
		/// 傳回所有動態成員名稱的列舉型別。
		/// </summary>
		/// <returns>
		/// 包含動態成員名稱的序列。
		/// </returns>
		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return _dr.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
		}

		/// <summary>
		/// 提供取得成員值之作業的實作。衍生自 <see cref="T:System.Dynamic.DynamicObject" /> 類別的類別可以覆寫這個方法，以指定取得屬性值這類作業的動態行為。
		/// </summary>
		/// <param name="binder">提供已呼叫動態作業之物件的相關資訊。binder.Name 屬性會提供其中執行動態作業之成員的名稱。例如，在 sampleObject 是衍生自 <see cref="T:System.Dynamic.DynamicObject" /> 類別之類別執行個體的 Console.WriteLine(sampleObject.SampleProperty) 陳述式中，binder.Name 會傳回 "SampleProperty"。binder.IgnoreCase 屬性會指定成員名稱是否區分大小寫。</param>
		/// <param name="result">取得作業的結果。例如，如果是針對屬性呼叫這個方法，您可以將屬性值指派給 <paramref name="result" />。</param>
		/// <returns>
		/// 如果作業成功，則為 true，否則為 false。如果這個方法傳回 false，語言的執行階段繫結器會決定行為。(在大部分情況下，會擲回執行階段例外狀況)。
		/// </returns>
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = _dr[binder.Name];
			return true;
		}
	}
}