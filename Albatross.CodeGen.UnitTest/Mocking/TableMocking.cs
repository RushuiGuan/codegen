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

		public Container Build(Container container = null) {
			if (container == null) { container = new Container(); }
			container.Options.AllowOverridingRegistrations = true;

			var getTableColumns = new Mock<IGetTableColumns>();
			getTableColumns.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(Columns);

			var getPrimaryKeys = new Mock<IGetTablePrimaryKey>();
			getPrimaryKeys.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(PrimaryKeys);

			var getIdentityColumn = new Mock<IGetTableIdentityColumn>();
			getIdentityColumn.Setup(args => args.Get(It.Is<DatabaseObject>(t=>t.Name == TableName))).Returns(IdentityColumn);

			container.RegisterSingleton<IGetTableColumns>(getTableColumns.Object);
			container.RegisterSingleton<IGetTablePrimaryKey>(getPrimaryKeys.Object);
			container.RegisterSingleton<IGetTableIdentityColumn>(getIdentityColumn.Object);
			container.RegisterSingleton<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>();
			container.RegisterSingleton<IGetVariableName, GetSqlVariableName>();
			return container;
		}
	}
}
