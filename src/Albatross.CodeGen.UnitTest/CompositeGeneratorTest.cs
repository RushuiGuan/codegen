﻿using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using Albatross.Test;
using Autofac;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class CompositeGeneratorTest: TestBase {
		protected Container container = new Container();


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
		public void CompositeTest() {
			Container container = Ioc.Container;
			var factory = container.GetInstance<IConfigurableCodeGenFactory>();
			factory.RegisterConstant("test1", GeneratorTarget.Any, "test1", null, null);
			factory.RegisterConstant("test2", GeneratorTarget.Any, "test2", null, null);
			factory.RegisterConstant("test3", GeneratorTarget.Any, "test3", null, null);
			factory.RegisterConstant("test4", GeneratorTarget.Any, "test4", null, null);
			factory.RegisterComposite(new Composite(typeof(Table), typeof(SqlCodeGenOption)) {
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
			factory.RegisterConstant();

			factory.Register(new CodeGenerator {
				GeneratorType = typeof(YieldTestCodeGenerator),
				SourceType = typeof(object),
				OptionType = typeof(object),
				Target = GeneratorTarget.Any,
				Name = "test0",
			});
			factory.RegisterConstant("test1", GeneratorTarget.Any, "test1", null, null);
			factory.RegisterConstant("test2", GeneratorTarget.Any, "test2", null, null);
			factory.RegisterConstant("test3", GeneratorTarget.Any, "test3", null, null);
			factory.RegisterConstant("test4", GeneratorTarget.Any, "test4", null, null);
			factory.RegisterConstant("test4", GeneratorTarget.Any, "test4", null, null);
			factory.RegisterComposite(new Composite(typeof(Table), typeof(SqlCodeGenOption)) {
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

		public override void Register(Container container) {
		new Albatross.CodeGen.SimpleInjector.Pack().RegisterServices(container);
		}
	}
}
