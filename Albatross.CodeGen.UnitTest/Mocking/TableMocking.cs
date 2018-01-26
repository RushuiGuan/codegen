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
		public abstract DatabaseObject Table { get; }


		public void Setup() {

			var getTableColumns = MockContainer.Instance.Get<IGetTableColumns>();
			getTableColumns.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(Columns);

			var getPrimaryKeys = MockContainer.Instance.Get<IGetTablePrimaryKey>();
			getPrimaryKeys.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(PrimaryKeys);

			var getIdentityColumn = MockContainer.Instance.Get<IGetTableIdentityColumn>();
			getIdentityColumn.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(IdentityColumn);
		}
	}
}
