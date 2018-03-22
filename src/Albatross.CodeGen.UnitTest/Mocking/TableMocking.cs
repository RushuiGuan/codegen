using Albatross.Database;
using Moq;
using System.Collections.Generic;

namespace Albatross.CodeGen.UnitTest.Mocking {
	public abstract class TableMocking: IMocking {
		public abstract string TableName { get; }
		public abstract string Schema { get; }
		public abstract IEnumerable<IndexColumn> PrimaryKeys { get; }
		public abstract IEnumerable<Column> Columns { get; }
		public abstract Column IdentityColumn { get; }

		Mock<IGetTable> getTable;

		public TableMocking(Mock<IGetTable> getTable) {
			this.getTable = getTable;
		}


		public void Setup() {
			getTable.Setup(args => args.Get(It.IsAny<Albatross.Database.Database>(), It.Is<string>(schema=> schema == Schema), It.Is<string>(name=>name == TableName))).Returns(new Table {
				Schema = Schema,
				Name= TableName,
				Columns = Columns,
				IdentityColumn = IdentityColumn,
				PrimaryKeys = PrimaryKeys,
			});
		}
	}
}
