using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Core;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(PropertyGenerator))]
	public class TestPropertyGenerator : TestBase{
		public override void Register(Container container) {
			container.Register<IRenderCSharp<DotNetType>, RenderDotNetType>();
			container.Register<IRenderCSharp<AccessModifier>, RenderAccessModifier>();
		}

		public readonly static Property Case1 = new Property {
			Name = "Name",
			Type = new DotNetType {
				Name = "string",
			},
		};
		public const string Case1Result = @"public string Name {
	get; set;
}";

		public readonly static Property Case2 = new Property {
			Name = "Name",
			Type = new DotNetType {
				Name = "string",
			},
			SetModifier = AccessModifier.Protected,
		};
		public const string Case2Result = @"public string Name {
	get; protected set;
}";

		public readonly static Property Case3 = new Property {
			Name = "Name",
			Type = new DotNetType {
				Name = "string",
			},
			SetModifier = AccessModifier.Private,
		};
		public const string Case3Result = @"public string Name {
	get; private set;
}";

		public readonly static Property Case4 = new Property {
			Name = "Name",
			Static = true,
			Type = new DotNetType {
				Name = "string",
			},
			SetModifier = AccessModifier.Private,
		};
		public const string Case4Result = @"public static string Name {
	get; private set;
}";


		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(Case1){ExpectedResult = Case1Result,},
				new TestCaseData(Case2){ExpectedResult = Case2Result,},
				new TestCaseData(Case3){ExpectedResult = Case3Result,},
				new TestCaseData(Case4){ExpectedResult = Case4Result,},
			};
		}



		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Property p) {
			var gen = Get<PropertyGenerator>();
			StringBuilder sb = new StringBuilder();
			gen.Build(sb, p, null);
			return sb.ToString();
		}
	}
}
