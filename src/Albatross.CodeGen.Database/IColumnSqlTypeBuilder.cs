using System.Text;

namespace Albatross.CodeGen.Database {
	public interface IColumnSqlTypeBuilder{
		string Build(Column c);
	}
}
