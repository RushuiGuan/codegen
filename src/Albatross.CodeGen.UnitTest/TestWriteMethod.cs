using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Core;
using Albatross.CodeGen.SimpleInjector;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf = typeof(WriteMethod))]
	public class TestWriteMethod : TestBase {
		public override void Register(Container container) {
			new Pack().RegisterServices(container);
		}

		public static IEnumerable<TestCaseData> GetTestCases() {
			TestCaseData case1 = new TestCaseData(new Method("Test")) {
				ExpectedResult = @"public void Test() {
}"
			};
			TestCaseData case2 = new TestCaseData(
				new Method("Test") {
					ReturnType = DotNetType.Integer,
					Parameters = new Parameter[] {
						new Parameter("a") {
							 Type = DotNetType.String,
						},
						new Parameter("b") {
							 Type = DotNetType.Integer,
						},
						new Parameter("c") {
							 Type = DotNetType.DateTime,
						},
					}
				}) {
				ExpectedResult = @"public int Test(string a, int b, DateTime c) {
}",
			};

			TestCaseData case3 = new TestCaseData(
				new Method("Test") {
					ReturnType = DotNetType.String,
					Parameters = new Parameter[] {
						new Parameter("a") {
							 Type = DotNetType.String,
						},
						new Parameter("b") {
							 Type = DotNetType.Integer,
						},
						new Parameter("c") {
							 Type = DotNetType.DateTime,
						},
					}
				}) {
				ExpectedResult = @"public string Test(string a, int b, DateTime c) {
}"
			};
			var constructor = new Method("Test");
			constructor.Body.Append("int i = 100;");

			TestCaseData case4 = new TestCaseData(constructor) {
				ExpectedResult = @"public void Test() {
	int i = 100;
}"
			};

			constructor = new Method("Test") {
				Static = true,
			};
			constructor.Body.Append("int i = 100;");
			TestCaseData case5 = new TestCaseData(constructor) {
				ExpectedResult = @"public static void Test() {
	int i = 100;
}"
			};
			return new TestCaseData[] {
				case1,
				case2,
				case3,
				case4,
				case5,
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Method p) {
			var writer = Get<WriteMethod>();
			return writer.Write(p);
		}
	}
}
