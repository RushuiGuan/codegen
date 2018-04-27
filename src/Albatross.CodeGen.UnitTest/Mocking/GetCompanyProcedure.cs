using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albatross.Database;
using Moq;

namespace Albatross.CodeGen.UnitTest.Mocking {
	public class GetCompanyProcedure : ProcedureMocking {
		public GetCompanyProcedure(Mock<IGetProcedure> getProcedure) : base(getProcedure) {
		}

		public override string Name => "GetCompany";
		public override string Schema => "ac";

		public override IEnumerable<Parameter> Parameters => new Parameter[] {
			new Parameter{
				Name = "user",
				 Type = new SqlType{
					  Name = "varchar",
					  MaxLength = 200,
				 }
			},
		};
	}
}