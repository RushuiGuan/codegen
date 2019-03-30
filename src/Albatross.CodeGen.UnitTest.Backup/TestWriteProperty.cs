using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Model;
using Albatross.CodeGen.CSharp.Writer;
using Albatross.CodeGen.SimpleInjector;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(WriteCSharpProperty))]
	public class TestWriteProperty : TestBase{
		public override void Register(Container container) {
			new Pack().RegisterServices(container);
		}

		public readonly static Property Case1 = new Property("Name") {
			Type = DotNetType.String,
		};
		public const string Case1Result = @"public string Name { get; set; }";

		public readonly static Property Case2 = new Property("Name") {
			Type = DotNetType.String,
			SetModifier = AccessModifier.Protected,
		};
		public const string Case2Result = @"public string Name { get; protected set; }";

		public readonly static Property Case3 = new Property("Name") {
			Type = DotNetType.String,
			SetModifier = AccessModifier.Private,
		};
		public const string Case3Result = @"public string Name { get; private set; }";

		public readonly static Property Case4 = new Property("Name") {
			Static = true,
			Type = DotNetType.String,
			SetModifier = AccessModifier.Private,
		};
		public const string Case4Result = @"public static string Name { get; private set; }";


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
			var writer = Get<WriteCSharpProperty>();
			return writer.Write(p);
		}
	}
}
