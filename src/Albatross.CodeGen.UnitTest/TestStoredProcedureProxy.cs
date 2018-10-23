using Albatross.CodeGen.CSharp.Core;
using Albatross.CodeGen.SimpleInjector;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TestStoredProcedureProxy : TestBase {

		public override void Register(Container container) {
			new Pack().RegisterServices(container);
		}

		public static TestCaseData Case1 = new TestCaseData(new Procedure() {
			Name = "Test",
			Parameters = new Albatross.Database.Parameter[] {
				 new Albatross.Database.Parameter {
					  Name = "id",
					   Type = new SqlType{ Name = "int"}
				 }
			 },
		}, new Class {
			Namespace = "Albatross.CodeGen",
		}) {
			ExpectedResult = @"using Dapper;
using System.Data;
namespace Albatross.CodeGen {
	public class Test {
		public CommandDefinition Create(int id) {
		}
	}
}",
		};

		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				Case1,
			};
		}


		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Procedure procedure, Class c) {
			StringBuilder sb = new StringBuilder();
			Get<StoredProcedureProxy>().Build(sb, procedure, c);
			return sb.ToString();
		}
	}
}
