using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.Database;
using Albatross.Database;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.procedure.alter", target: GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Create a SQL server stored procedure", SourceType = typeof(object))]
	public class AlterProcedure : CreateAlterProcedure {
		public AlterProcedure(IRetrieveSqlVariable getVariable, IRenderSqlVariable renderVariable, IRenderSqlParameter renderParameter) : base(getVariable, renderVariable, renderParameter) {
		}

		public override string Action => "alter";
	}
}


