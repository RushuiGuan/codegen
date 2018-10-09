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
					Variables = new Variable[] {
						new Variable("a") {
							 Type = DotNetType.String,
						},
						new Variable("b") {
							 Type = DotNetType.Integer,
						},
						new Variable("c") {
							 Type = DotNetType.DateTime,
						},
					}
				}) {
				ExpectedResult = @"public Test(string @a, int @b, DateTime @c) {
}",
			};

			TestCaseData case3 = new TestCaseData(
				new Constructor("Test") {
					Name = "Test",
					Variables = new Variable[] {
						new Variable("a") {
							 Type = DotNetType.String,
						},
						new Variable("b") {
							 Type = DotNetType.Integer,
						},
						new Variable("c") {
							 Type = DotNetType.DateTime,
						},
					},
					ChainedConstructor = new Constructor("this") {
						Variables = new Variable[] {
							new Variable("a"),
							new Variable("b")
						}
					}
				}) {
				ExpectedResult = @"public Test(string @a, int @b, DateTime @c) : this(@a, @b) {
}"
			};
			var constructor = new Constructor("Test");
			constructor.Body.Append("int i = 100;");

			TestCaseData case4 = new TestCaseData(constructor) {
				ExpectedResult = @"public Test() {
	int i = 100;
}"
			};

			constructor = new Constructor("Test") {
				Static = true,
			};
			constructor.Body.Append("int i = 100;");
			TestCaseData case5 = new TestCaseData(constructor) {
				ExpectedResult = @"static Test() {
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
		public string Run(Constructor p) {
			var writer = Get<WriteConstructor>();
			return writer.Write(p);
		}
	}
}
 