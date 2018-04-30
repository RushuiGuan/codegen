using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	public class ConvertDataType : IConvertDataType {
		public ConvertDataType() {
			Init();
		}
		Dictionary<string, Tuple<Type, DbType>> mapping = new Dictionary<string, Tuple<Type, DbType>>();

		void Init() {
			mapping.Add("bigint", new Tuple<Type, DbType>(typeof(long), DbType.Int64));
			mapping.Add("binary", new Tuple<Type, DbType>(typeof(byte[]), DbType.Binary));
			mapping.Add("bit", new Tuple<Type, DbType>(typeof(Boolean), DbType.Boolean));
			mapping.Add("char", new Tuple<Type, DbType>(typeof(string), DbType.AnsiStringFixedLength));
			mapping.Add("date", new Tuple<Type, DbType>(typeof(DateTime), DbType.Date));
			mapping.Add("datetime", new Tuple<Type, DbType>(typeof(DateTime), DbType.DateTime));
			mapping.Add("datetime2", new Tuple<Type, DbType>(typeof(DateTime), DbType.DateTime2));
			mapping.Add("datetimeoffset", new Tuple<Type, DbType>(typeof(DateTimeOffset), DbType.DateTimeOffset));
			mapping.Add("decimal", new Tuple<Type, DbType>(typeof(decimal), DbType.Decimal));
			mapping.Add("float", new Tuple<Type, DbType>(typeof(double), DbType.Double));
			mapping.Add("image", new Tuple<Type, DbType>(typeof(byte[]), DbType.Binary));
			mapping.Add("int", new Tuple<Type, DbType>(typeof(int), DbType.Int32));
			mapping.Add("money", new Tuple<Type, DbType>(typeof(decimal), DbType.Decimal));
			mapping.Add("nchar", new Tuple<Type, DbType>(typeof(string), DbType.StringFixedLength));
			mapping.Add("ntext", new Tuple<Type, DbType>(typeof(string), DbType.String));
			mapping.Add("numeric", new Tuple<Type, DbType>(typeof(decimal), DbType.Decimal));
			mapping.Add("nvarchar", new Tuple<Type, DbType>(typeof(string), DbType.String));
			mapping.Add("real", new Tuple<Type, DbType>(typeof(Single), DbType.Single));
			mapping.Add("rowversion", new Tuple<Type, DbType>(typeof(byte[]), DbType.Binary));
			mapping.Add("smalldatetime", new Tuple<Type, DbType>(typeof(DateTime), DbType.DateTime));
			mapping.Add("smallint", new Tuple<Type, DbType>(typeof(Int16), DbType.Int16));
			mapping.Add("smallmoney", new Tuple<Type, DbType>(typeof(decimal), DbType.Decimal));
			mapping.Add("sql_variant", new Tuple<Type, DbType>(typeof(object), DbType.Object));
			mapping.Add("text", new Tuple<Type, DbType>(typeof(string), DbType.String));
			mapping.Add("time", new Tuple<Type, DbType>(typeof(TimeSpan), DbType.Time));
			mapping.Add("timestamp", new Tuple<Type, DbType>(typeof(byte[]), DbType.Binary));
			mapping.Add("tinyint", new Tuple<Type, DbType>(typeof(byte), DbType.Byte));
			mapping.Add("uniqueidentifier", new Tuple<Type, DbType>(typeof(Guid), DbType.Guid));
			mapping.Add("varbinary", new Tuple<Type, DbType>(typeof(byte[]), DbType.Binary));
			mapping.Add("varchar", new Tuple<Type, DbType>(typeof(string), DbType.AnsiString));
			mapping.Add("xml", new Tuple<Type, DbType>(typeof(string), DbType.Xml));
		}


		public Type GetDotNetType(SqlType sqlType) {
			Tuple<Type, DbType> tuple;
			if (mapping.TryGetValue(sqlType.Name, out tuple)) {
				return tuple.Item1;
			} else {
				throw new NotSupportedException();
			}
		}

		public DbType GetDbType(SqlType sqlType) {
			Tuple<Type, DbType> tuple;
			if (mapping.TryGetValue(sqlType.Name, out tuple)) {
				return tuple.Item2;
			} else {
				throw new NotSupportedException();
			}
		}
	}
}
