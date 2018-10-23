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
	[TestFixture(TestOf =typeof(WriteField))]
	public class TestWriteField : TestBase{
		public override void Register(Container container) {
			new Pack().RegisterServices(container);
		}

		public readonly static Field Case1 = new Field("Name") {
			Type = DotNetType.String,
		};
		public const string Case1Result = @"public string Name;";

		public readonly static Field Case2 = new Field("Name") {
			Type = DotNetType.String,
			ReadOnly = true,
		};
		public const string Case2Result = @"public readonly string Name;";

		public readonly static Field Case3 = new Field("Name") {
			Type = DotNetType.String,
			ReadOnly = true,
		};
		public const string Case3Result = "public readonly string Name = \"test\";";

		static TestWriteField() {
			Case3.Assignment.Append("\"test\"");
		}

		public readonly static Field Case4 = new Field("Name") {
			Static = true,
			Type = DotNetType.String,
		};
		public const string Case4Result = @"public static string Name;";


		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(Case1){ExpectedResult = Case1Result,},
				new TestCaseData(Case2){ExpectedResult = Case2Result,},
				new TestCaseData(Case3){ExpectedResult = Case3Result,},
				new TestCaseData(Case4){ExpectedResult = Case4Result,},
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Field p) {
			var writer = Get<WriteField>();
			return writer.Write(p);
		}
	}
}
