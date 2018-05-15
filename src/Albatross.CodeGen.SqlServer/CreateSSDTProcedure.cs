using Albatross.CodeGen.Core;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.procedure.ssdt", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "create procedure statement for ssdt environment")]
	public class CreateSSDTProcedure : ICodeGenerator<Procedure, ICodeGeneratorOption> {
		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }
		public int TabLevel { get; set; }

		public void Configure(object data) {
		}

		public IEnumerable<object> Generate(StringBuilder sb, Procedure source, ICodeGeneratorOption option) {
			if (source == null) {
				throw new Faults.CodeGeneratorException("missing source");
			}
			if (string.IsNullOrEmpty(source.CreateScript)) {
				throw new Faults.CodeGeneratorException("missing stored procedure create script");
			} else {
				sb.AppendLine(source.CreateScript);
			}
			sb.AppendLine("go");
			if (!string.IsNullOrEmpty(source.PermissionScript)) {
				sb.AppendLine(source.PermissionScript);
				sb.AppendLine("go");
			}
			return new object[] { this, };
		}

		public IEnumerable<object> Generate(StringBuilder sb, object source, ICodeGeneratorOption option) {
			return this.ValidateNGenerate(sb, source, option);
		}
	}
}
