using System;
using System.Collections.Generic;
using System.IO;
using Albatross.CodeGen.CSharp.Model;
using Albatross.CodeGen.CSharp.Writer;
using NUnit.Framework;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Conversion;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(WriteDotNetType))]
	public class WriteDotNetTypeTest {

		static IEnumerable<TestCaseData> Get() {
			return new TestCaseData[]{
				new TestCaseData(DotNetType.Boolean()){
					ExpectedResult = "bool",
				},
				new TestCaseData(DotNetType.Byte()){
					ExpectedResult = "byte"
				},
				new TestCaseData(DotNetType.ByteArray()){
					ExpectedResult = "byte[]"
				},
				new TestCaseData(DotNetType.Char()){
					ExpectedResult ="char",
				},
				new TestCaseData(DotNetType.DateTime()){
					ExpectedResult = "DateTime",
				},
				new TestCaseData(DotNetType.DateTimeOffset()){
					ExpectedResult = "DateTimeOffset",
				},
				new TestCaseData(DotNetType.Decimal()){
					ExpectedResult = "decimal",
				},
				new TestCaseData(DotNetType.Double()){
					ExpectedResult = "System.Double",
				},
				new TestCaseData(DotNetType.Guid()){
					ExpectedResult = "Guid",
				},
				new TestCaseData(DotNetType.IDbConnection()){
					ExpectedResult = typeof(System.Data.IDbConnection).FullName,
				},
				new TestCaseData(DotNetType.Integer()){
					ExpectedResult = "System.Int32",
				},
				new TestCaseData(DotNetType.Long()){
					ExpectedResult = "long",
				},
				new TestCaseData(DotNetType.Short()){
					ExpectedResult = "short",
				},
				new TestCaseData(DotNetType.Single()){
					ExpectedResult = "single",
				},
				new TestCaseData(DotNetType.String()){
					ExpectedResult = "System.String",
				},
				new TestCaseData(DotNetType.TimeSpan()){
					ExpectedResult = "TimeSpan",
				},
				new TestCaseData(DotNetType.Void()){
					ExpectedResult = "void",
				},
				new TestCaseData(DotNetType.MakeIEnumerable(DotNetType.String())){ ExpectedResult="System.Collections.Generic.IEnumerable<System.String>" },
				new TestCaseData(new DotNetType("System.Int32", true, false, null)){ ExpectedResult="System.Int32[]" },
				new TestCaseData(DotNetType.MakeNullable(DotNetType.Integer())){ ExpectedResult= "System.Nullable<System.Int32>" },
			};
		}

		[TestCaseSource(nameof(Get))]
		public string Run(DotNetType dotNetType) {
			
			StringWriter writer = new StringWriter();
			writer.Run(new WriteDotNetType(), dotNetType);
			return writer.ToString();
		}
	}
}
