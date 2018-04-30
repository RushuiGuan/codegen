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
	[CodeGenerator("create stored procedure", target: GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Create a SQL server stored procedure", SourceType = typeof(object))]
	public class CreateStoredProcedure : ICodeGenerator<object, SqlCodeGenOption> {
		IGetVariable getVariable;
		IBuildVariable buildVariable;
		IBuildParameter buildParameter;

		public CreateStoredProcedure(IGetVariable getVariable, IBuildVariable buildVariable, IBuildParameter buildParameter) {
			this.getVariable = getVariable;
			this.buildVariable = buildVariable;
			this.buildParameter = buildParameter;
		}

		public event Func<StringBuilder, IEnumerable<object>> Yield;

		public IEnumerable<object> Build(StringBuilder sb, object source, SqlCodeGenOption option) {
			StringBuilder content = new StringBuilder();
			var items = Yield?.Invoke(content);
			if (items == null) {
				items = new object[0];
			}

			IEnumerable<Variable> variables = option.Variables?? new Variable[0];

			foreach (var item in items) {
				variables = variables.Union(getVariable.Get(item));
			}

			sb.Append("create procedure ").EscapeName(option.Schema).Dot().EscapeName(option.Name).AppendLine();
			foreach (var param in option.Parameters) {
				buildParameter.Build(sb.Tab(), param);
				if (variables.Count() > 0 || param != option.Parameters.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			foreach (var variable in variables) {
				buildVariable.Build(sb.Tab(), variable);

				if (variable != variables.Last()) {
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

			return new[] { this }.Union(items);
		}

		public void Configure(object data) {
		}
	}
}


