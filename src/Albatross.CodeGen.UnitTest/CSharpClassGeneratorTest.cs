using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Generation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(ClassInterfaceGenerator<>))]
	public class CSharpClassGeneratorTest {

		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(new CSharpClassOption{ Namespace = "Albatross.Test" }){ ExpectedResult = @"using System;
namespace Albatross.Test {
	public class Test {
		public string Name { get; set; }
	}
}
" },
				new TestCaseData(new CSharpClassOption()){ ExpectedResult = @"using System;
public class Test {
	public string Name { get; set; }
}
" },
				new TestCaseData(
					new CSharpClassOption{
						Namespace = "Albatross.Test",
						AccessModifier = "protected internal",
						Inheritance = new string[]{ "System.List<Object>", "IDisposable",  },
						Constructors = new string[]{
							"(int a) { this.a = a; }"
						},
						Imports = new string[]{
							"System",
							"Dapper"
						},
					}){ ExpectedResult = @"using System;
using Dapper;
namespace Albatross.Test {
	protected internal class Test : System.List<Object>, IDisposable {
		public Test(int a) { this.a = a; }
		public string Name { get; set; }
	}
}
" },
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string RunClassGeneratorTest(CSharpClassOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance< TestCSharpClass >().Generate(sb, null, option);
			return sb.ToString();
		}
	}
}
