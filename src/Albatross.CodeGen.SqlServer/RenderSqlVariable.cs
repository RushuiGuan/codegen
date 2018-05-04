using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class RenderSqlVariable : IRenderSqlVariable {
		IRenderSqlType buildSqlType;
		ICreateSqlVariableName createVariableName;

		public RenderSqlVariable(IRenderSqlType buildSqlType, ICreateSqlVariableName createVariableName) {
			this.buildSqlType = buildSqlType;
			this.createVariableName = createVariableName;
		}

		public StringBuilder Render(StringBuilder sb, Variable variable) {
			return sb.Append(createVariableName.Get(variable.Name)).Space().Append("as ").Append(buildSqlType.Render(variable.Type));
		}
	}
}
