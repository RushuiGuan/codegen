using Albatross.CodeGen.CSharp;
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
    public class CommandDefinitionClassTest
    {
		public static IEnumerable<TestCaseData> GetTestCases() {
			IGetProcedure getProcedure = Ioc.Container.GetInstance<IGetProcedure>();

			return new TestCaseData[] {
				new TestCaseData(getProcedure.Get(new Albatross.Database.Database(), "ac", "getcompany"), new ClassOption()){
					ExpectedResult = "",
				}
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Procedure p, ClassOption option) {
			StringBuilder sb = new StringBuilder();
			new CommandDefinitionClass(new GetCSharpType()).Build(sb, p, option);
			return sb.ToString();
		}
    }
}
