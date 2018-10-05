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
	[TestFixture(TestOf = typeof(WriteConstructor))]
	public class TestWriteConstructor : TestBase {
		public override void Register(Container container) {
			new Pack().RegisterServices(container);
		}

		public static IEnumerable<TestCaseData> GetTestCases() {
			TestCaseData case1 = new TestCaseData(new Constructor("Test")) {
				ExpectedResult = @"public Test() {
}"
			};
			TestCaseData case2 = new TestCaseData(
				new Constructor("Test") {
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
				ExpectedResult = @"public Test(string a, int b, DateTime c) {
}",
			};

			TestCaseData case3 = new TestCaseData(
				new Constructor("Test") {
					Name = "Test",
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
					},
					ChainedConstructor = new Constructor("this") {
						Parameters = new Parameter[] {
							new Parameter("a"),
							new Parameter("b")
						}
					}
				}) {
				ExpectedResult = @"public Test(string a, int b, DateTime c) : this(a, b) {
}"
			};
			var constructor = new Constructor("Test");
			constructor.Body.Append("int i = 100;");

			TestCaseData case4 = new TestCaseData(constructor) {
				ExpectedResult = @"public Test() {
	int i = 100;
}"
			};
			return new TestCaseData[] {
				case1,
				case2,
				case3,
				case4,
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Constructor p) {
			var writer = Get<WriteConstructor>();
			return writer.Write(p);
		}
	}
}
