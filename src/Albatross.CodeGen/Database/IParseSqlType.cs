using Albatross.Database;

namespace Albatross.CodeGen.Database {
	public interface IParseSqlType
    {
		SqlType Parse(string text);
    }
}
