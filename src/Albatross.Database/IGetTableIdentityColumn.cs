namespace Albatross.Database {
	public interface IGetTableIdentityColumn {
		Column Get(Server server, string schema, string table);
	}
}
