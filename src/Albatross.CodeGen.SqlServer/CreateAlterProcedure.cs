using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.Database;
using Albatross.Database;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.SqlServer {
	public abstract class CreateAlterProcedure : ICodeGenerator<object, SqlCodeGenOption> {
		IRetrieveSqlVariable getVariable;
		IRenderSqlVariable renderVariable;
		IRenderSqlParameter renderParameter;

		public CreateAlterProcedure(IRetrieveSqlVariable getVariable, IRenderSqlVariable renderVariable, IRenderSqlParameter renderParameter) {
			this.getVariable = getVariable;
			this.renderVariable = renderVariable;
			this.renderParameter = renderParameter;
		}

		public abstract string Action { get; }

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

			sb.Append(Action).Append(" procedure ").EscapeName(option.Schema).Dot().EscapeName(option.Name).AppendLine();
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
			sb.Append("as begin").AppendLine();

			string text = content.ToString();
			if (!string.IsNullOrEmpty(text)) {
				sb.Tabify(text, 1);
				sb.AppendLine();
			}
			sb.AppendLine("end");
			return new[] { this }.Union(items);
		}

		public IEnumerable<object> Generate(StringBuilder sb, object source, object option) {
			return this.ValidateNGenerate(sb, source, option);
		}

		public void Configure(object data) {
		}
	}
}


