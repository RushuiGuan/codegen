using Albatross.Database;
using System.Text;

namespace Albatross.CodeGen.Database {
	public interface IBuildSqlType {
		string Build(SqlType type);
	}
}
