using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Moq;
using NUnit.Framework;
using SimpleInjector;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class CompositeGeneratorTest {
		protected Container container = new Container();

		[OneTimeSetUp]
		public void Setup() {
		}


		[Test]
		public void CompositeTest() {
			Container container = Ioc.Container;
			var factory = container.GetInstance<IConfigurableCodeGenFactory>();
			factory.RegisterStatic("test1", GeneratorTarget.Any, "test1", null, null);
			factory.RegisterStatic("test2", GeneratorTarget.Any, "test2", null, null);
			factory.RegisterStatic("test3", GeneratorTarget.Any, "test3", null, null);
			factory.RegisterStatic("test4", GeneratorTarget.Any, "test4", null, null);
			factory.Register(new Composite<DatabaseObject, SqlQueryOption> {
				 Name = "test", 
				  Generators = new[] {"test1","test2" },
				   Target = "sql"
			});

			var handle = factory.Create<DatabaseObject, SqlQueryOption>("test");
			IEnumerable<object> used;
			var result = handle.Build(new StringBuilder(), new DatabaseObject(), new SqlQueryOption(), factory, out used);
			Assert.AreEqual("test1test2", result.ToString());
			Assert.Greater(used.Count(), 1);
		}
	}
}
