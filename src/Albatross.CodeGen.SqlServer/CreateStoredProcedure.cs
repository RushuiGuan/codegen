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

		public CreateStoredProcedure(IGetVariable getVariable) {
			this.getVariable = getVariable;
		}

		public event Func<StringBuilder, IEnumerable<object>> Yield;

		public IEnumerable<object> Build(StringBuilder sb, DatabaseObject source, SqlCodeGenOption option) {
			StringBuilder text = new StringBuilder();
			var items = Yield?.Invoke(text);
			if (items == null) {
				items = new object[0];
			}

			Dictionary<string, string> variables = new Dictionary<string, string>();

			foreach (var item in items) {
				var dict = getVariable.Get(item);
				foreach (var v in dict) {
					variables.Add(v.Key, v.Value);
				}
			}

			sb.Append("create procedure ").EscapeName(option.Schema).Dot().EscapeName(option.Name).Space().OpenParenthesis().AppendLine();
			foreach (string key in variables.Keys) {
				sb.Tab().Append(key).Append(" as ").Append(variables[key]);
				if (key != variables.Keys.Last()) {
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


