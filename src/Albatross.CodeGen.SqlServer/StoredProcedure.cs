using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.Database;
using Albatross.Database;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.SqlServer {
	/// <summary>
	/// Create a stored procedure based on the variables produced by the next generator in a composite generator.  The generator will yield for 1 turn.
	/// </summary>
	[CodeGenerator("sql.procedure", target: GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Create a SQL server stored procedure", SourceType = typeof(object))]
	public class StoredProcedure : ICodeGenerator<object, SqlCodeGenOption> {
		IRetrieveSqlVariable getVariable;
		IRenderSqlVariable renderVariable;
		IRenderSqlParameter renderParameter;

		public StoredProcedure(IRetrieveSqlVariable getVariable, IRenderSqlVariable renderVariable, IRenderSqlParameter renderParameter) {
			this.getVariable = getVariable;
			this.renderVariable = renderVariable;
			this.renderParameter = renderParameter;
		}

		public event Func<StringBuilder, IEnumerable<object>> Yield;

		public IEnumerable<object> Generate(StringBuilder sb, object source, SqlCodeGenOption option) {
			StringBuilder content = new StringBuilder();
			var items = Yield?.Invoke(content);
			if (items == null) {
				items = new object[0];
			}

			IEnumerable<Variable> variables = option.Variables?? new Variable[0];
			IEnumerable<Parameter> parameters = option.Parameters ?? new Parameter[0];

			foreach (var item in items) {
				variables = variables.Union(getVariable.Retrieve(item));
			}

			sb.Append("create procedure ").EscapeName(option.Schema).Dot().EscapeName(option.Name).AppendLine();
			foreach (var variable in variables) {
				renderVariable.Render(sb.Tab(), variable);

				if (parameters.Count() > 0 || variable != variables.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			foreach (var param in parameters) {
				renderParameter.Render(sb.Tab(), param);
				if (param != option.Parameters.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			sb.Append("as").AppendLine();

			string text = content.ToString();
			if (!string.IsNullOrEmpty(text)) {
				sb.Tabify(text, 1);
				sb.AppendLine();
			}
			sb.Append("go");

			if (option.GrantPermission) {
				sb.AppendLine();
				sb.Append("grant exec on ").EscapeName(option.Schema).Dot().EscapeName(option.Name).Append(" to ").Append(option.Principals).AppendLine();
				sb.AppendLine("go");
			}
			return new[] { this }.Union(items);
		}

		public IEnumerable<object> Generate(StringBuilder sb, object source, object option) {
			return this.ValidateNBuild(sb, source, option);
		}

		public void Configure(object data) {
		}
	}
}


