using Albatross.Database;
using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.permission", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Generate sql permission for an object")]
	public class RenderDatabasePermission : ICodeGenerator<IDatabaseObject, Object> {
		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }

		public void Configure(object data) {
		}

		public IEnumerable<object> Generate(StringBuilder sb, IDatabaseObject source, object option) {

			if (source.Permissions?.Count() > 0) {
				foreach (var permission in source.Permissions) {
					sb.Append(permission.State.ToLower()).Space().Append(permission.Permission.ToLower()).Space().Append("on ").EscapeName(source.Schema).Dot().EscapeName(source.Name).Append(" to ").EscapeName(permission.Principal).AppendLine();
				}
			}
			return new object[] { this };
		}

		public IEnumerable<object> Generate(StringBuilder sb, object source, object option) {
			return this.ValidateNGenerate(sb, source, option);
		}
	}
}
