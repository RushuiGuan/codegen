using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.Database;

namespace Albatross.CodeGen.SqlServer {
	/// <summary>
	/// Create a stored procedure based on the variables produced by the next generator in a composite generator.  The generator will yield for 1 turn.
	/// </summary>
	[CodeGenerator("create stored procedure", target: GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Create a SQL server stored procedure", SourceType = typeof(DatabaseObject))]
	public class CreateStoredProcedure : ICodeGenerator<DatabaseObject, Database.SqlCodeGenOption> {
		IGetVariable getVariable;
		IBuildVariable buildVariable;

		public CreateStoredProcedure(IGetVariable getVariable, IBuildVariable buildVariable) {
			this.getVariable = getVariable;
			this.buildVariable = buildVariable;
		}

		public event Func<StringBuilder, IEnumerable<object>> Yield;

		public IEnumerable<object> Build(StringBuilder sb, DatabaseObject source, SqlCodeGenOption option) {
			StringBuilder text = new StringBuilder();
			var items = Yield?.Invoke(text);
			if (items == null) {
				items = new object[0];
			}

			IEnumerable<Variable> variables = new Variable[0];

			foreach (var item in items) {
				variables = variables.Union(getVariable.Get(item));
			}

			sb.Append("create procedure ").EscapeName(option.Schema).Dot().EscapeName(option.Name).Space().OpenParenthesis().AppendLine();
			foreach (var item in variables) {
				buildVariable.Build(sb.Tab(), item);
				if (item != variables.Last()) {
					sb.Comma().AppendLine();
				}
			}
			sb.AppendLine().CloseParenthesis().Append(" as ").AppendLine();

			sb.Tabify(text.ToString(), 1);

			sb.AppendLine();
			sb.AppendLine("go");

			return new[] { this }.Union(items);
		}

		public void Configure(object data) {
		}
	}
}


