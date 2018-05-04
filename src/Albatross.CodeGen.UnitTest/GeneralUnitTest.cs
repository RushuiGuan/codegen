using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using NUnit.Framework;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
		public class GeneralUnitTest {
			static TestCaseData[] GetBuildSqlTypeTestCase() {
				return new TestCaseData[] {
					new TestCaseData(new SqlType(){ Name = "bigint", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "bigint" },
					new TestCaseData(new SqlType(){ Name = "binary", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "binary(8)" },
					new TestCaseData(new SqlType(){ Name = "bit", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "bit" },
					new TestCaseData(new SqlType(){ Name = "char", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "char(8)" },
					new TestCaseData(new SqlType(){ Name = "date", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "date" },
					new TestCaseData(new SqlType(){ Name = "datetime", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "datetime" },
					new TestCaseData(new SqlType(){ Name = "datetime2", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "datetime2(3)" },
					new TestCaseData(new SqlType(){ Name = "datetimeoffset", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "datetimeoffset(3)" },
					new TestCaseData(new SqlType(){ Name = "decimal", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "decimal(3, 2)" },
					new TestCaseData(new SqlType(){ Name = "float", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "float(3)" },
					new TestCaseData(new SqlType(){ Name = "geography", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "geography" },
					new TestCaseData(new SqlType(){ Name = "geometry", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "geometry" },
					new TestCaseData(new SqlType(){ Name = "hierarchyid", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "hierarchyid" },
					new TestCaseData(new SqlType(){ Name = "image", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "varbinary(max)" },
					new TestCaseData(new SqlType(){ Name = "int", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "int" },
					new TestCaseData(new SqlType(){ Name = "money", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "money" },
					new TestCaseData(new SqlType(){ Name = "nchar", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "nchar(8)" },
					new TestCaseData(new SqlType(){ Name = "ntext", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "nvarchar(max)" },
					new TestCaseData(new SqlType(){ Name = "numeric", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "numeric(3, 2)" },
					new TestCaseData(new SqlType(){ Name = "nvarchar", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "nvarchar(8)" },
					new TestCaseData(new SqlType(){ Name = "real", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "real" },
					new TestCaseData(new SqlType(){ Name = "smalldatetime", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "smalldatetime" },
					new TestCaseData(new SqlType(){ Name = "smallint", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "smallint" },
					new TestCaseData(new SqlType(){ Name = "smallmoney", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "smallmoney" },
					new TestCaseData(new SqlType(){ Name = "sql_variant", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "sql_variant" },
					new TestCaseData(new SqlType(){ Name = "sysname", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "sysname" },
					new TestCaseData(new SqlType(){ Name = "text", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "varchar(max)" },
					new TestCaseData(new SqlType(){ Name = "time", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "time" },
					new TestCaseData(new SqlType(){ Name = "timestamp", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "timestamp" },
					new TestCaseData(new SqlType(){ Name = "tinyint", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "tinyint" },
					new TestCaseData(new SqlType(){ Name = "uniqueidentifier", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "uniqueidentifier" },
					new TestCaseData(new SqlType(){ Name = "varbinary", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "varbinary(8)" },
					new TestCaseData(new SqlType(){ Name = "varchar", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "varchar(8)" },
					new TestCaseData(new SqlType(){ Name = "xml", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "xml" },
					new TestCaseData(new SqlType(){ Name = "SvcRef", Schema="dyn", IsTableType = true, }){ ExpectedResult = "[dyn].[SvcRef]" },
			};
		}

		[TestCaseSource(nameof(GetBuildSqlTypeTestCase))]
		public string BuildSqlTypeTest(SqlType type) {
			RenderSqlType builder = new RenderSqlType();
			return builder.Render(type);
		}

		static TestCaseData[] GetBuildSqlVariableTestCase() {
			return new TestCaseData[] {
					new TestCaseData(new Variable{ Name="name",  Type=  new SqlType(){ Name = "varchar", MaxLength = 8, Precision = 3, Scale = 2 } }){ ExpectedResult = "@name as varchar(8)" } ,
					new TestCaseData(new Variable{ Name = "svc", Type = new SqlType(){ Name = "SvcRef", Schema="dyn", IsTableType = true, } }){ ExpectedResult = "@svc as [dyn].[SvcRef]" },
			};
		}

		[TestOf(typeof(RenderSqlVariable))]
		[TestCaseSource(nameof(GetBuildSqlVariableTestCase))]
		public string BuildSqlVariableTest(Variable variable) {
			RenderSqlVariable b = new RenderSqlVariable(new RenderSqlType(), new CreateSqlVariableName());
			StringBuilder sb = new StringBuilder();
			b.Render(sb, variable);
			return sb.ToString();
		}

		static TestCaseData[] BuildProcedureParameterTestCase() {
			return new TestCaseData[] {
					new TestCaseData(new Parameter{ Name="name",  Type=  new SqlType(){ Name = "varchar", MaxLength = 8, Precision = 3, Scale = 2 } }){ ExpectedResult = "@name as varchar(8)" } ,
					new TestCaseData(new Parameter{ Name = "svc", Type = new SqlType(){ Name = "SvcRef", Schema="dyn", IsTableType = true, } }){ ExpectedResult = "@svc as [dyn].[SvcRef] readonly" },
			};
		}

		[TestOf(typeof(RenderSqlParameter))]
		[TestCaseSource(nameof(BuildProcedureParameterTestCase))]
		public string BuildProcedureParameterTest(Parameter param) {
			RenderSqlParameter b = new RenderSqlParameter(new RenderSqlType(), new CreateSqlVariableName());
			StringBuilder sb = new StringBuilder();
			b.Render(sb, param);
			return sb.ToString();
		}
	}
}