using Albatross.Database;

namespace Albatross.CodeGen.CSharp.Core {
	public interface IConvertSqlTypeToDotNetType {
		DotNetType Get(SqlType sqlType);
	}
}
