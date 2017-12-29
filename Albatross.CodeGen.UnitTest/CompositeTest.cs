﻿using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Moq;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	public class CompositeTest {
		protected Container container = new Container();
		protected Mock<IGetTableColumns> mock_GetTableColumns = new Mock<IGetTableColumns>();
		protected Mock<IGetTableIdentityColumn> mock_GetTableIDColumn = new Mock<IGetTableIdentityColumn>();
		protected Mock<IGetTablePrimaryKey> mock_GetTablePrimaryKey = new Mock<IGetTablePrimaryKey>();

		public CompositeTest() {
			container.Options.AllowOverridingRegistrations = true;

			container.RegisterSingleton<IGetTableColumns, GetTableColumns>();
			container.RegisterSingleton<IGetVariableName, GetSqlVariableName>();
			container.RegisterSingleton<IGetTablePrimaryKey, GetTablePrimaryKey>();
			container.RegisterSingleton<IGetTableIdentityColumn, GetTableIdentityColumn>();
			container.RegisterSingleton<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>();

			container.RegisterCollection<BuiltInColumn>(BuiltInColumns.Items);
			container.RegisterSingleton<IBuiltInColumnFactory, BuiltInColumnFactory>();


			container.Register<ICodeGeneratorFactory, ContainerControlledCodeGenFactory>(Lifestyle.Singleton);
			container.Register<IGetTablePrimaryKey>(() => mock_GetTablePrimaryKey.Object, Lifestyle.Singleton);
			container.Register<IGetTableIdentityColumn>(() => mock_GetTableIDColumn.Object, Lifestyle.Singleton);
			container.Register<IGetTableColumns>(() => mock_GetTableColumns.Object, Lifestyle.Singleton);

			container.Verify();
		}
	}
}
