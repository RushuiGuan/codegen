using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
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
			factory.Register(new Composite(typeof(Table), typeof(SqlCodeGenOption)) {
				Name = "test",
				Branch = new Branch(new Leaf("test1"), new Leaf("test2")),
				Target = GeneratorTarget.Sql,
			});

			var handle = factory.Create<Table, SqlCodeGenOption>("test");
			StringBuilder sb = new StringBuilder();
			var used = handle.Build(sb, new Table(), new SqlCodeGenOption());
			Assert.AreEqual("test1test2", sb.ToString());
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

			StringBuilder sb = new StringBuilder();
			IEnumerable<object> used = handle.Build(sb, string.Empty, string.Empty);
			Assert.AreEqual("begin\r\n\r\nend", sb.ToString());
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
			factory.Register(new Composite(typeof(Table), typeof(SqlCodeGenOption)) {
				Name = "test",
				Branch = new Branch(new Leaf("test0"), new Leaf("test1"), new Leaf("newline"), new Leaf("test2")),
				Target = GeneratorTarget.Sql,
			});

			var handle = factory.Create<Table, SqlCodeGenOption>("test");
			StringBuilder sb = new StringBuilder();
			IEnumerable<object> used= handle.Build(sb, new Table(), new SqlCodeGenOption());
			Assert.AreEqual(@"begin
	test1	
	test2
end", sb.ToString());
			Assert.Greater(used.Count(), 1);
		}
	}
}
