using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class RenderSqlParameter : IRenderSqlParameter{
		IRenderSqlType buildSqlType;
		ICreateSqlVariableName createVariableName;

		public RenderSqlParameter(IRenderSqlType buildSqlType, ICreateSqlVariableName createVariableName) {
			this.buildSqlType = buildSqlType;
			this.createVariableName = createVariableName;
		}

		public StringBuilder Render(StringBuilder sb, Parameter param) {
			sb.Append(createVariableName.Get(param.Name)).Space().Append("as ").Append(buildSqlType.Render(param.Type));
			if (param.Type.IsTableType) {
				sb.Space().Append("readonly");
			}
			return sb;
		}
	}
}
