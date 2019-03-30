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
	[TestFixture(TestOf =typeof(WriteCSharpClass))]
	public class TestWriteClass : TestBase{
		public override void Register(Container container) {

			new Pack().RegisterServices(container);
		}

		public readonly static Class Case1 = new Class("Test") {
			Namespace = "Albatross.Test",
			Dependencies = new Dependency[] {
				new Dependency("dbConn") {
					Type = new DotNetType("WarehouseDbConnection"),
					FieldType = DotNetType.IDbConnection,
				}
			},
		};
		public const string Case1Result = @"namespace Albatross.Test {
	public class Test {
		public Test(WarehouseDbConnection @dbConn) {
			this.dbConn = @dbConn;
		}
		private System.Data.IDbConnection dbConn;
	}
}";

		
		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(Case1){ExpectedResult = Case1Result,},
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Class p) {
			var writer = Get<WriteCSharpClass>();
			string result = writer.Write(p);
			return result;
		}
	}
}
