using Albatross.Database;
using System.Text;

namespace Albatross.CodeGen.Database {
	/// <summary>
	/// The interface will generate the sql text for a <see cref="Albatross.Database.SqlType"/> object
	/// </summary>
	public interface IBuildSqlType {
		string Build(SqlType type);
	}
}
