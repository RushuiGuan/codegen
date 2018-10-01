using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Core;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf = typeof(WriteDotNetType))]
	public class TestRenderDotNetType : TestBase {
		public override void Register(Container container) {
		}

		readonly static DotNetType Case1 = new DotNetType {
			Name = "string",
		};
		const string Case1Result = @"string";

		readonly static DotNetType Case2 = new DotNetType {
			Name = "IEnumerable",
			IsGeneric = true,
			GenericTypes = new DotNetType[] {
				new DotNetType{Name = "string"},
				new DotNetType{Name = "int"},
			},
		};
		const string Case2Result = @"IEnumerable<string, int>";


		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(Case1){ExpectedResult = Case1Result,},
				new TestCaseData(Case2){ExpectedResult = Case2Result,},
			};
		}



		[TestCaseSource(nameof(GetTestCases))]
		public string Run(DotNetType t) {
			var gen = Get<WriteDotNetType>();
			return gen.Write(t);
		}
	}
}