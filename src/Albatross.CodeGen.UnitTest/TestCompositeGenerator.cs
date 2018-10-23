using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TestCompositeGenerator:TestBase {
		public override void Register(Container container) {
			new Albatross.CodeGen.SimpleInjector.Pack().RegisterServices(container);
		}

		[Test]
		public void TestNormalComposite() {
			var composite = new Composite {
				Branch = new Branch(new INode[] {
							new Leaf("string"),
							new Leaf("string"),
							new Leaf("string"),
							new Leaf("string"),
						}),
				Name = "test",
			};
			Get<IConfigurableCodeGenFactory>().RegisterAssembly(typeof(IConfigurableCodeGenFactory).Assembly);
			StringBuilder sb = new StringBuilder();
			Get<IRunCodeGenerator>().Run(composite.GetMeta(), sb, "test", null);
			sb.ToString();
		}

		[Test]
		public void Test2() {
			var composite = new Composite {
				Branch = new Branch(new INode[] {
							new Leaf("create-stored-procedure"),
							new Leaf("table-insert"),
						}),
				Name = "test",
			};

			Get<IConfigurableCodeGenFactory>().RegisterAssembly(typeof(IConfigurableCodeGenFactory).Assembly);
			Get<IConfigurableCodeGenFactory>().RegisterAssembly(typeof(TableInsert).Assembly);
			StringBuilder sb = new StringBuilder();
			Table table = new Table {
				Name = "Company",
				Schema = "access",
				Database = new Albatross.Database.Database {
					DataSource = "localhost",
					InitialCatalog = "Albatross",
					SSPI = true,
				},
			};
			SqlCodeGenOption option = new SqlCodeGenOption {
				Name = "test",
				Schema = "ss",
			};
			Get<IRunCodeGenerator>().Run(composite.GetMeta(), sb, table, option);
			sb.ToString();
		}
	}
}
