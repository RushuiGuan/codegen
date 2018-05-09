using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Generation;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(CSharpNamespace))]
	public class CSharpNamespaceTest {

		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(new CSharpClassOption{ Namespace = "Albatross.Test" }){ ExpectedResult = @"using System;
namespace Albatross.Test {
	public class Test {
		public string Name { get; set; }
	}
	public class Test {
		public string Name { get; set; }
	}
}
" },
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string RunClassGeneratorTest(CSharpClassOption option) {
			

			Container c = Ioc.Container;
			var factory = c.GetInstance<IConfigurableCodeGenFactory>();
			factory.Register(new Composite(typeof(object), typeof(CSharpClassOption)) {
				Name = "test",
				Branch = new Branch(new Leaf("csharp.namespace"), new Branch(new Leaf("csharp.class.test"), new Leaf("csharp.class.test"))),
				Target = GeneratorTarget.Sql,
			});

			StringBuilder sb = new StringBuilder();
			var handle = factory.Create<object, CSharpClassOption>("test");
			handle.Generate(sb, null, option);
			return sb.ToString();
		}
	}
}
