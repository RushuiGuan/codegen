using Albatross.Database;

namespace Albatross.CodeGen.CSharp.Core {
	public interface IGetDotNetType {
		DotNetType Get(SqlType sqlType);
	}
}
