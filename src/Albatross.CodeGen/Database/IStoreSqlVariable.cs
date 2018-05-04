using Albatross.Database;

namespace Albatross.CodeGen.Database {
	/// <summary>
	/// The interface allow the code generator to stored a variable that can be retrieved later by using the <see cref="Albatross.CodeGen.Database.IRetrieveSqlVariable" /> interface.
	/// </summary>
	public interface IStoreSqlVariable {
		void Store(object owner, Variable variable);
    }
}
