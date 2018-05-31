using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Routing;

namespace EF6_UnitOfWork
{
	public static class DataTableExt
	{
		/// <summary>
		/// To the data table.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static DataTable ToDataTable<T>(this IList<T> data)
		{
			PropertyDescriptorCollection properties =
				TypeDescriptor.GetProperties(typeof(T));
			DataTable table = new DataTable();
			foreach (PropertyDescriptor prop in properties)
				table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
			foreach (T item in data)
			{
				DataRow row = table.NewRow();
				foreach (PropertyDescriptor prop in properties)
					row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
				table.Rows.Add(row);
			}
			return table;
		}


		/// <summary>
		/// 使用SQL語法查詢，並取得DataTable結果
		/// 
		/// sample:
		/// db.QueryDataTable("exec sp_Query @verId", new
		///	{
		///		@verId = whtatever,
		///	});
		/// </summary>
		/// <param name="db">The database.</param>
		/// <param name="commandText">The command text.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <returns></returns>
		public static DataTable QueryDataTable(this DbContext db, string commandText, dynamic parameters = null, CommandType commandType = CommandType.Text)
		{
			DataTable dt = new DataTable();
			var conn = db.Database.Connection;
			if (conn.State != ConnectionState.Open)
				conn.Open();
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandText = commandText;
				cmd.CommandType = commandType;

				if (parameters != null)
				{
					var dict = new RouteValueDictionary(parameters);
					foreach (var key in dict.Keys)
					{
						cmd.Parameters.Add(new SqlParameter(key, dict[key]));
					}
				}
				using (var reader = cmd.ExecuteReader())
				{
					dt.Load(reader);
				}
			}
			return dt;
		}

		/// <summary>
		/// 使用SQL語法查詢，並取得動態類別物件結果
		/// 
		/// sample:
		/// db.QueryDataTable("exec sp_Query @verId", new
		///	{
		///		@verId = whtatever,
		///	});
		/// </summary>
		/// <param name="db">The database.</param>
		/// <param name="commandText">The command text.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <returns></returns>
		public static IEnumerable<dynamic> QueryDynamic(this DbContext db, string commandText, dynamic parameters = null, CommandType commandType = CommandType.Text)
		{
			DataTable dt = QueryDataTable(db, commandText, parameters, commandType);
			return dt.Rows.Cast<DataRow>().Select(dr => new DynamicDataRowObject(dr));
		}
	}
}