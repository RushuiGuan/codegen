using System.Text;

namespace Albatross.CodeGen.Database {
	public interface IColumnSqlTypeBuilder{
		StringBuilder Build(StringBuilder sb, Column c);
	}
}
