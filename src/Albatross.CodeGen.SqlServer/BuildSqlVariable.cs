using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class BuildSqlVariable : IBuildSqlVariable {
		IBuildSqlType buildSqlType;
		ICreateSqlVariableName createVariableName;

		public BuildSqlVariable(IBuildSqlType buildSqlType, ICreateSqlVariableName createVariableName) {
			this.buildSqlType = buildSqlType;
			this.createVariableName = createVariableName;
		}

		public StringBuilder Build(StringBuilder sb, Variable variable) {
			return sb.Append(createVariableName.Get(variable.Name)).Space().Append("as ").Append(buildSqlType.Build(variable.Type));
		}
	}
}
