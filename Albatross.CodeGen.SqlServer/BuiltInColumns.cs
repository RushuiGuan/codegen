using Albatross.CodeGen.Database;

namespace Albatross.CodeGen.SqlServer {
	public static class BuiltInColumns {
		public readonly static BuiltInColumn[] Items = new BuiltInColumn[] {
			new BuiltInStringColumn("createdBy"),
			new BuiltInStringColumn("modifiedBy"),
			new BuiltInDateTimeColumn("created"),
			new BuiltInDateTimeColumn("modified"),
		};
	}
}
