using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Generation;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest
{
	[TestFixture]
    public class ProcedureCommandDefinitionClassTest
    {
		public static IEnumerable<TestCaseData> GetTestCases() {
			IGetProcedure getProcedure = Ioc.Container.GetInstance<IGetProcedure>();

			return new TestCaseData[] {
				new TestCaseData(getProcedure.Get(new Albatross.Database.Database(), "ac", "getcompany"), new CSharpClassOption{ Namespace = "test", Imports = new string[]{ "System", "Dapper" } }){
					ExpectedResult = @"using System;
using Dapper;
namespace test {
	public class GetCompany {
		public GetCompany(string user, System.Data.IDbTransaction transaction = null) {
			DynamicParameters dynamicParameters = new DynamicParameters();
			dynamicParameters.Add(""user"" , user, dbType:System.Data.DbType.AnsiString);
			Definition = new CommandDefinition(""[ac].[GetCompany]"", dynamicParameters, commandType:CommandType.StoredProcedure, transaction:transaction);
		}
		public CommandDefinition Definition { get; private set; }
	}
}
",
				}
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Procedure p, CSharpClassOption option) {
			StringBuilder sb = new StringBuilder();
			new ProcedureCommandDefinitionClass(new ConvertDataType(), new RenderDotNetType()).Generate(sb, new Dictionary<string, string>(), p, option);
			return sb.ToString();
		}
    }
}
