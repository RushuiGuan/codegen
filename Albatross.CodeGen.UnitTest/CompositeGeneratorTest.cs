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
				Branch = new Branch(new Leaf("test1"), new Leaf("test2")),
				Target = GeneratorTarget.Sql,
			});

			var handle = factory.Create<DatabaseObject, SqlQueryOption>("test");
			IEnumerable<object> used;
			var result = handle.Build(new StringBuilder(), new DatabaseObject(), new SqlQueryOption(), factory, out used);
			Assert.AreEqual("test1test2", result.ToString());
			Assert.Greater(used.Count(), 1);
		}

		[Test]
		public void EmptyYieldTest() {
			Container container = Ioc.Container;
			var factory = container.GetInstance<IConfigurableCodeGenFactory>();
			factory.Register(new CodeGenerator {
				GeneratorType = typeof(YieldTestCodeGenerator),
				SourceType = typeof(object),
				OptionType = typeof(object),
				Target = GeneratorTarget.Any,
				Name = "test0",
			});
			var handle = factory.Create<string, string>("test0");
			IEnumerable<object> used;
			var result = handle.Build(new StringBuilder(), string.Empty, string.Empty, factory, out used);
			Assert.AreEqual("begin\r\n\r\nend", result.ToString());
			Assert.AreEqual(1, used.Count());
		}

		[Test]
		public void CompositeYieldTest() {
			Container container = Ioc.Container;
			var factory = container.GetInstance<IConfigurableCodeGenFactory>();
			factory.RegisterStatic();

			factory.Register(new CodeGenerator {
				GeneratorType = typeof(YieldTestCodeGenerator),
				SourceType = typeof(object),
				OptionType = typeof(object),
				Target = GeneratorTarget.Any,
				Name = "test0",
			});
			factory.RegisterStatic("test1", GeneratorTarget.Any, "test1", null, null);
			factory.RegisterStatic("test2", GeneratorTarget.Any, "test2", null, null);
			factory.RegisterStatic("test3", GeneratorTarget.Any, "test3", null, null);
			factory.RegisterStatic("test4", GeneratorTarget.Any, "test4", null, null);
			factory.RegisterStatic("test4", GeneratorTarget.Any, "test4", null, null);
			factory.Register(new Composite<DatabaseObject, SqlQueryOption> {
				Name = "test",
				Branch = new Branch(new Leaf("test0"), new Leaf("test1"), new Leaf("newline"), new Leaf("test2")),
				Target = GeneratorTarget.Sql,
			});


			var handle = factory.Create<DatabaseObject, SqlQueryOption>("test");
			IEnumerable<object> used;
			var result = handle.Build(new StringBuilder(), new DatabaseObject(), new SqlQueryOption(), factory, out used);
			Assert.AreEqual(@"begin
	test1	
	test2
end", result.ToString());
			Assert.Greater(used.Count(), 1);
		}
	}
}
