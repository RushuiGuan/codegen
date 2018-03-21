using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using NUnit.Framework;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
		public class ColumnSqlTypeBuilderTest {
			static TestCaseData[] GetTestCase() {
				return new TestCaseData[] {
					new TestCaseData(new Column(){ DataType = "bigint", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "bigint" },
					new TestCaseData(new Column(){ DataType = "binary", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "binary(8)" },
					new TestCaseData(new Column(){ DataType = "bit", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "bit" },
					new TestCaseData(new Column(){ DataType = "char", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "char(8)" },
					new TestCaseData(new Column(){ DataType = "date", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "date" },
					new TestCaseData(new Column(){ DataType = "datetime", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "datetime" },
					new TestCaseData(new Column(){ DataType = "datetime2", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "datetime2(3)" },
					new TestCaseData(new Column(){ DataType = "datetimeoffset", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "datetimeoffset(3)" },
					new TestCaseData(new Column(){ DataType = "decimal", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "decimal(3, 2)" },
					new TestCaseData(new Column(){ DataType = "float", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "float(3)" },
					new TestCaseData(new Column(){ DataType = "geography", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "geography" },
					new TestCaseData(new Column(){ DataType = "geometry", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "geometry" },
					new TestCaseData(new Column(){ DataType = "hierarchyid", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "hierarchyid" },
					new TestCaseData(new Column(){ DataType = "image", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "varbinary(max)" },
					new TestCaseData(new Column(){ DataType = "int", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "int" },
					new TestCaseData(new Column(){ DataType = "money", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "money" },
					new TestCaseData(new Column(){ DataType = "nchar", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "nchar(8)" },
					new TestCaseData(new Column(){ DataType = "ntext", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "nvarchar(max)" },
					new TestCaseData(new Column(){ DataType = "numeric", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "numeric(3, 2)" },
					new TestCaseData(new Column(){ DataType = "nvarchar", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "nvarchar(8)" },
					new TestCaseData(new Column(){ DataType = "real", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "real" },
					new TestCaseData(new Column(){ DataType = "smalldatetime", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "smalldatetime" },
					new TestCaseData(new Column(){ DataType = "smallint", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "smallint" },
					new TestCaseData(new Column(){ DataType = "smallmoney", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "smallmoney" },
					new TestCaseData(new Column(){ DataType = "sql_variant", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "sql_variant" },
					new TestCaseData(new Column(){ DataType = "sysname", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "sysname" },
					new TestCaseData(new Column(){ DataType = "text", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "varchar(max)" },
					new TestCaseData(new Column(){ DataType = "time", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "time" },
					new TestCaseData(new Column(){ DataType = "timestamp", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "timestamp" },
					new TestCaseData(new Column(){ DataType = "tinyint", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "tinyint" },
					new TestCaseData(new Column(){ DataType = "uniqueidentifier", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "uniqueidentifier" },
					new TestCaseData(new Column(){ DataType = "varbinary", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "varbinary(8)" },
					new TestCaseData(new Column(){ DataType = "varchar", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "varchar(8)" },
					new TestCaseData(new Column(){ DataType = "xml", MaxLength = 8, Precision = 3, Scale = 2 }){ ExpectedResult = "xml" },
			};
		}

		[TestCaseSource(nameof(GetTestCase))]
		public string Run(Column column) {
			BuildSqlType builder = new BuildSqlType();
			return builder.Build(column);
		}
	}
}