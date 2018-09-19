using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class BuildProcedureParameter : IBuildParameter{
		IBuildSqlType buildSqlType;
		ICreateVariableName createVariableName;

		public BuildProcedureParameter(IBuildSqlType buildSqlType, ICreateVariableName createVariableName) {
			this.buildSqlType = buildSqlType;
			this.createVariableName = createVariableName;
		}

		public StringBuilder Build(StringBuilder sb, Parameter param) {
			sb.Append(createVariableName.Get(param.Name)).Space().Append("as ").Append(buildSqlType.Build(param.Type));
			if (param.Type.IsTableType) {
				sb.Space().Append("readonly");
			}
			return sb;
		}
	}
}
