using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Moq;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest.Mocking {
	public abstract class TableMocking: IMocking {
		public abstract string TableName { get; }
		public abstract string Schema { get; }
		public abstract IEnumerable<Column> PrimaryKeys { get; }
		public abstract IEnumerable<Column> Columns { get; }
		public abstract Column IdentityColumn { get; }

		Mock<IGetTableColumns> getTableColumns;
		Mock<IGetTablePrimaryKey> getPrimaryKeys;
		Mock<IGetTableIdentityColumn> getIdentityColumn;

		public TableMocking(Mock<IGetTableColumns> getTableColumns, Mock<IGetTablePrimaryKey> getPrimaryKeys, Mock<IGetTableIdentityColumn> getIdentityColumn) {
			this.getTableColumns = getTableColumns;
			this.getPrimaryKeys = getPrimaryKeys;
			this.getIdentityColumn = getIdentityColumn;
		}


		public void Setup() {
			getTableColumns.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(Columns);
			getPrimaryKeys.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(PrimaryKeys);
			getIdentityColumn.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(IdentityColumn);
		}
	}
}
